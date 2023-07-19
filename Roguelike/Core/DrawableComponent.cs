using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguelike.Components;

namespace Roguelike.Core;

/// <summary>
///     Интерфейс, отвечающий за всё, что отрисовывается.
/// </summary>
public abstract class DrawableComponent : Component
{
    /// <summary>
    ///     Порядок отрисовки.
    /// </summary>
    public abstract int DrawOrder { get; set; }

    /// <summary>
    ///     Метод, вызывающийся каждый кадр.
    /// </summary>
    public abstract void Draw(GameTime time, SpriteBatch batch);
}