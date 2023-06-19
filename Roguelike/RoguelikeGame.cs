using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Roguelike.Actors.InventoryUtils;
using Roguelike.Core;
using Roguelike.World;
using Roguelike.World.Providers.Generator;

namespace Roguelike;

/// <summary>
///     Класс, отвечающий за игру. Здесь расположен так называемый главный цикл.
/// </summary>
public sealed class RoguelikeGame : BaseGame
{
    private static readonly string ContentRoot = "Content";
    private static readonly string MapsFolder = Path.Combine(ContentRoot, "Maps");

    /// <summary>
    ///     Констуктор. Здесь происходит инициализация игрового окна.
    /// </summary>
    public RoguelikeGame()
    {
        Content.RootDirectory = ContentRoot;
        IsMouseVisible = true;
    }

    /// <summary>
    ///     Инициализация игры.
    /// </summary>
    protected override void Initialize()
    {
        var builder = new DefaultWorldBuilder();
        var generatorParams = GeneratorParams.Default;
        generatorParams.RoomSize = 5;
        generatorParams.Seed = (uint)Random.Shared.NextInt64();
        builder.SetupSource(new GeneratedWorldSource(15, 15, generatorParams));
        builder.SetupWorldObjects(new DefaultObjectsProvider());

        World = builder.Build(this);

        var inventory = new Inventory(this);
        Components.Add(inventory);
        base.Initialize();
    }

    /// <summary>
    ///     Обновление игровой логики. gameTime - текущее игровое время.
    /// </summary>
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    /// <summary>
    ///     Отрисовка.
    /// </summary>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);
    }
}