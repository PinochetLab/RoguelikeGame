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

namespace Roguelike;

public sealed class RoguelikeGame : Game
{
    public static RoguelikeGame Instance { get; private set; }

    private static readonly List<Actor> Actors = new();
    private static readonly List<CanvasActor> CanvasActors = new();

    private static readonly List<Actor> ActorsForRemove = new();


    private readonly GraphicsDeviceManager graphics;

    private Texture2D floorTexture;
    public SpriteBatch SpriteBatch { get; private set; }

    public RoguelikeGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = FieldInfo.ScreenWith;
        graphics.PreferredBackBufferHeight = FieldInfo.ScreenHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Instance = this;
    }

    public Texture2D LoadTexture(string path)
    {
        return Content.Load<Texture2D>(path);
    }

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

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        floorTexture = Content.Load<Texture2D>("GrassTile");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var deltaTime = GetDeltaTime(gameTime);

        base.Update(gameTime);

        foreach (var actor in Actors)
            actor.Update(deltaTime);

        foreach (var actor in CanvasActors)
            actor.Update(deltaTime);

        RemoveRemovedActors();
    }

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

        foreach (var actor in Actors)
            actor.Draw(delta);

        for (var i = 1; i < cellCount; i++)
        {
            SpriteBatch.DrawLine(new Vector2(0, i * cellSize), new Vector2(size, i * cellSize), Color.Black, 2);
            SpriteBatch.DrawLine(new Vector2(i * cellSize, 0), new Vector2(i * cellSize, size), Color.Black, 2);
        }

        foreach (var actor in CanvasActors)
            actor.Draw(delta);

        SpriteBatch.End();
    }

    public static void AddActor(Actor actor)
    {
        Actors.Add(actor);
    }

    public static void AddCanvasActor(CanvasActor actor)
    {
        CanvasActors.Add(actor);
    }

    public static void RemoveActor(Actor actor)
    {
        ActorsForRemove.Add(actor);
    }

    private static void RemoveRemovedActors()
    {
        foreach (var actor in ActorsForRemove)
            Actors.Remove(actor);
        ActorsForRemove.Clear();
    }

    private static float GetDeltaTime(GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
}