using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Roguelike.Core;

namespace Roguelike.Components;

/// <summary>
/// Компонент, отвечающий за такие вещи, как позиция, размер и поворот объекта.
/// </summary>
public class TransformComponent : Component
{
    private Vector2Int position = Vector2Int.Zero;

    private TransformComponent parent;


    /// <summary>
    /// Родительский элемент.
    /// </summary>
    public TransformComponent Parent
    {
        get => parent;
        set
        {
            parent?.Children.Remove(this);
            parent = value;
            parent?.Children.Add(this);
        }
    }

    /// <summary>
    /// Позиция.
    /// </summary>
    public Vector2Int Position
    {
        get => position;
        set
        {
            Vector2Int offset = value - position;
            position = value;
            Children.ForEach(child => child.Position += offset);
        }
    }

    /// <summary>
    /// Дочерние элементы.
    /// </summary>
    public List<TransformComponent> Children = new();

    /// <summary>
    /// Scale объекта (во сколько раз он больше, чем оригинальная версия).
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    /// Flip по горизонтали.
    /// </summary>
    public bool FlipX
    {
        set
        {
            var s = Scale;
            s.X = MathF.Abs(s.X) * (value ? -1 : 1);
            Scale = s;
        }
    }

    /// <summary>
    /// Flip по вертикали.
    /// </summary>
    public bool FlipY
    {
        set
        {
            var s = Scale;
            s.Y = MathF.Abs(s.Y) * (value ? -1 : 1);
            Scale = s;
        }
    }

    /// <summary>
    /// Угол поворота, относительно оси Z.
    /// </summary>
    public float Angle { get; set; } = 0;
}
