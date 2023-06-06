using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Roguelike.Field;
using Roguelike.Actors;
using Roguelike.Actors.InventoryUtils;
using IDrawable = Roguelike.Components.IDrawable;
using System.Linq;
using Roguelike.World;

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


    private readonly GraphicsDeviceManager graphics;
    private readonly WorldComponent _worldComponent;

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

        _worldComponent = new WorldComponent(this);
        Components.Add(_worldComponent);
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

        var inventoryPosition = new Vector2Int(FieldInfo.ScreenWith / 2, FieldInfo.ScreenWith);
        Actor.Create<Inventory>(inventoryPosition);
    }

    /// <summary>
    /// Загрузка контента.
    /// </summary>
    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
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

        foreach (var actor in Actors)
            actor.Update(deltaTime);

        RemoveRemovedActors();
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

        _drawables = _drawables.OrderBy(x => x.Canvas ? 100 : 0 + x.DrawOrder).ToList();
        _drawables.ForEach(x => x.Draw(delta));

        SpriteBatch.End();
    }

    /// <summary>
    /// Метод отвечающий за добавление нового объекта, требующего отрисовки, для последующей работы с ним.
    /// </summary>
    public static void AddDrawable(IDrawable drawable)
    {
        _drawables.Add(drawable);
    }

    /// <summary>
    /// Метод, отвечащий за добавление нового игрового объекта для последующей работы с ним.
    /// </summary>
    public static void AddActor(Actor actor)
    {
        Actors.Add(actor);
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

    private static void RemoveRemovedActors()
    {
        ActorsForRemove.ForEach(x => Actors.Remove(x));
        ActorsForRemove.Clear();

        DrawablesForRemove.ForEach(x => _drawables.Remove(x));
        DrawablesForRemove.Clear();
    }

    public static float GetDeltaTime(GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
}