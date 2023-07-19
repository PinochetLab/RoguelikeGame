using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Roguelike.Core;
using Roguelike.Field;

namespace Roguelike.Components;

/// <summary>
///     Компонент, отвечающий за такие вещи, как позиция, размер и поворот объекта.
/// </summary>
public class TransformComponent : Component
{
    /// <summary>
    ///     Дочерние элементы.
    /// </summary>
    public List<TransformComponent> Children = new();

    private TransformComponent parent;
    private Vector2Int position = Vector2Int.Zero;

    private bool positionSet;

    /// <summary>
    ///     Если данное поле истино, объект является объектом на тайловом поле, его спрайт будет иметь позицию соответствующей
    ///     клетки,
    ///     а размер будет браться за размер клетки.
    /// </summary>
    public bool IsCanvas { get; set; } = false;


    /// <summary>
    ///     Родительский элемент.
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
    ///     Позиция.
    /// </summary>
    public Vector2Int Position
    {
        get => position;
        set
        {
            if (!positionSet)
            {
                positionSet = true;
                position = value;
                return;
            }

            var offset = value - position;
            position = value;
            foreach (var child in Children)
                if (!IsCanvas && child.IsCanvas) child.Position += offset * FieldInfo.CellSizeVector;
                else child.Position += offset;
        }
    }

    /// <summary>
    ///     Scale объекта (во сколько раз он больше, чем оригинальная версия).
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;


    /// <summary>
    ///     Положение на экране.
    /// </summary>
    public Vector2 ScreenPosition => FieldInfo.CellSizeVector * Position + FieldInfo.CellSizeVector * 0.5f;

    /// <summary>
    ///     Flip по горизонтали.
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
    ///     Flip по вертикали.
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
    ///     Угол поворота, относительно оси Z.
    /// </summary>
    public float Angle { get; set; }

    public Vector2Int Direction
    {
        get => new((int)MathF.Cos(Angle), (int)MathF.Sin(Angle));
        set => Angle = MathF.Atan2(value.Y, value.X);
    }
}