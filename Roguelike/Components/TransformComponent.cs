using Microsoft.Xna.Framework;
using Roguelike.Core;

namespace Roguelike.Components;

/// <summary>
/// Компонент, отвечающий за такие вещи, как позиция, размер и поворот объекта.
/// </summary>
public class TransformComponent : Component
{
    /// <summary>
    /// Позиция.
    /// </summary>
    public Vector2Int Position { get; set; } = Vector2Int.Zero;

    /// <summary>
    /// Scale объекта (во сколько раз он больше, чем оригинальная версия).
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    /// Угол поворота, относительно оси Z.
    /// </summary>
    public float Angle { get; set; } = 0;
}
