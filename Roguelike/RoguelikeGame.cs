using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Roguelike.Actors.InventoryUtils;
using Roguelike.Core;
using Roguelike.World;

namespace Roguelike;

/// <summary>
/// Класс, отвечающий за игру. Здесь расположен так называемый главный цикл.
/// </summary>
public sealed class RoguelikeGame : BaseGame
{
    /// <summary>
    /// Констуктор. Здесь происходит инициализация игрового окна.
    /// </summary>
    public RoguelikeGame()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    /// <summary>
    /// Инициализация игры.
    /// </summary>
    protected override void Initialize()
    {
        World = new WorldComponent(this);

        var inventory = new Inventory(this);
        Components.Add(inventory);
        base.Initialize();
    }

    /// <summary>
    /// Обновление игровой логики. gameTime - текущее игровое время.
    /// </summary>
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    /// <summary>
    /// Отрисовка.
    /// </summary>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);
    }
}