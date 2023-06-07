using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.Net;
using MonoGame.Extended;
using Roguelike.Field;
using Roguelike.Actors;
using Roguelike.Actors.InventoryUtils;
using System.Runtime.CompilerServices;
using Roguelike.Components;
using IDrawable = Roguelike.Components.IDrawable;
using System.Linq;
using IMovable = Roguelike.Components.IMovable;

namespace Roguelike;

/// <summary>
/// Класс, отвечающий за игру. Здесь расположен так называемый главный цикл.
/// </summary>
public sealed class RoguelikeGame : Game
{

    /// <summary>
    /// Синглтон.
    /// </summary>
    public static RoguelikeGame Instance { get; private set; }

    private static readonly List<Actor> Actors = new();
    private static List<IDrawable> _drawables = new();

    private static readonly List<Actor> ActorsForRemove = new();
    private static readonly List<IDrawable> DrawablesForRemove = new();

    private static readonly List<Actor> ActorsForAdd = new();
    private static readonly List<IDrawable> DrawablesForAdd = new();


    private readonly GraphicsDeviceManager graphics;

    private Texture2D floorTexture;

    /// <summary>
    /// SpriteBatch, необходимый для отрисовки.
    /// </summary>
    public SpriteBatch SpriteBatch { get; private set; }

    /// <summary>
    /// Констуктор. Здесь происходит инициализация игрового окна.
    /// </summary>
    public RoguelikeGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = FieldInfo.ScreenWith;
        graphics.PreferredBackBufferHeight = FieldInfo.ScreenHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Instance = this;
    }

    /// <summary>
    /// Метод, необходимый для загрузки текстуры.
    /// </summary>
    public Texture2D LoadTexture(string path)
    {
        return Content.Load<Texture2D>(path);
    }

    /// <summary>
    /// Инициализация игры.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        LoadContent();

        Actor.Create<ItemHolder>(7, 3);

        Actor.Create<Hero>(FieldInfo.Center);


        for (var i = 0; i < FieldInfo.CellCount; i++)
        {
            Actor.Create<Wall>(0, i);
            Actor.Create<Wall>(FieldInfo.CellCount - 1, i);
            if (i > 0 && i < FieldInfo.CellCount - 1)
            {
                Actor.Create<Wall>(i, 0);
                Actor.Create<Wall>(i, FieldInfo.CellCount - 1);
            }
        }

        var inventoryPosition = new Vector2Int(FieldInfo.ScreenWith / 2, FieldInfo.ScreenWith);
        Actor.Create<Inventory>(inventoryPosition);
    }

    /// <summary>
    /// Загрузка контента.
    /// </summary>
    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        floorTexture = Content.Load<Texture2D>("GrassTile");
    }

    /// <summary>
    /// Обновление игровой логики. gameTime - текущее игровое время.
    /// </summary>
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var deltaTime = GetDeltaTime(gameTime);

        base.Update(gameTime);

        Debug.WriteLine(Actors.Count);

        foreach (var actor in Actors)
            actor.Update(deltaTime);

        AddAndRemoveObjects();
    }

    /// <summary>
    /// Метод, который вызывается при совершении главным героем хода. Двигает все подвижные игровые объекты.
    /// </summary>
    public static void MoveAll()
    {
        foreach (var actor in Actors)
        {
            if (actor is IMovable movable)
            {
                movable.Move();
            }
        }
    }

    /// <summary>
    /// Отрисовка.
    /// </summary>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);

        var delta = GetDeltaTime(gameTime);

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        var size = FieldInfo.ScreenWith;
        var cellCount = FieldInfo.CellCount;
        var cellSize = FieldInfo.CellSize;

        for (var i = 0; i < cellCount; i++)
        for (var j = 0; j < cellCount; j++)
        {
            var tex = floorTexture;
            var rectangle = new Rectangle(i * cellSize, j * cellSize, 1 * cellSize, 1 * cellSize);
            var rectSize = new Rectangle(0, 0, tex.Width, tex.Height);
            SpriteBatch.Draw(tex, rectangle, rectSize, Color.WhiteSmoke);
        }
        

        for (var i = 1; i < cellCount; i++)
        {
            SpriteBatch.DrawLine(new Vector2(0, i * cellSize), new Vector2(size, i * cellSize), Color.Black, 2);
            SpriteBatch.DrawLine(new Vector2(i * cellSize, 0), new Vector2(i * cellSize, size), Color.Black, 2);
        }

        _drawables = _drawables.OrderBy(x => x.Canvas ? 100 : 0 + x.DrawOrder).ToList();
        _drawables.ForEach(x => x.Draw(delta));

        SpriteBatch.End();
    }

    /// <summary>
    /// Метод отвечающий за добавление нового объекта, требующего отрисовки, для последующей работы с ним.
    /// </summary>
    public static void AddDrawable(IDrawable drawable)
    {
        DrawablesForAdd.Add(drawable);
    }

    /// <summary>
    /// Метод, отвечащий за добавление нового игрового объекта для последующей работы с ним.
    /// </summary>
    public static void AddActor(Actor actor)
    {
        ActorsForAdd.Add(actor);
    }

    /// <summary>
    /// Метод, отвечающий за удаление (забывание) объекта, требующего отрисовки.
    /// </summary>
    public static void RemoveDrawable(IDrawable drawable)
    {
        DrawablesForRemove.Add(drawable);
    }

    /// <summary>
    /// Метод, отвечающий за удаление (забывание) игрового объекта.
    /// </summary>
    public static void RemoveActor(Actor actor)
    {
        ActorsForRemove.Add(actor);
    }

    private static void AddAndRemoveObjects()
    {
        ActorsForRemove.ForEach(x => Actors.Remove(x));
        ActorsForRemove.Clear();

        DrawablesForRemove.ForEach(x => _drawables.Remove(x));
        DrawablesForRemove.Clear();

        ActorsForAdd.ForEach(x => Actors.Add(x));
        ActorsForAdd.Clear();

        DrawablesForAdd.ForEach(x => _drawables.Add(x));
        DrawablesForAdd.Clear();
    }

    private static float GetDeltaTime(GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
}