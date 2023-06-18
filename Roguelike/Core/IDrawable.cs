using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roguelike.Core;

/// <summary>
///     Интерфейс, отвечающий за всё, что отрисовывается.
/// </summary>
public interface IDrawable
{
    /// <summary>
    ///     Порядок отрисовки.
    /// </summary>
    public int DrawOrder { get; set; }

    /// <summary>
    ///     Является ли объект канвас элементом.
    /// </summary>
    public bool Canvas { get; set; }

    /// <summary>
    ///     Метод, вызывающийся каждый кадр.
    /// </summary>
    public void Draw(GameTime time, SpriteBatch batch);
}