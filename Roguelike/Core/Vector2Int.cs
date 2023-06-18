﻿using System;
using Microsoft.Xna.Framework;

namespace Roguelike.Core;

/// <summary>
///     Структура, необходимая для работы с целочисленными двумерными векторами. Достойной альтернативы не нашлось, так как
///     нужны касты в дробные
///     вектора и обратно, а также вспомогательные вектора, такие как Left, Up, Unit и т.д.
/// </summary>
public record struct Vector2Int
{
    /// <summary>
    ///     Конструктор от двух координат.
    /// </summary>
    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    ///     X-координата.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    ///     Y-координата.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    ///     Длина вектора (гиппотенуза).
    /// </summary>
    public float Length => MathF.Sqrt(X * X + Y * Y);

    /// <summary>
    ///     Вектор (0, 0).
    /// </summary>
    public static Vector2Int Zero => new(0, 0);

    /// <summary>
    ///     Вектор (1, 0)
    /// </summary>
    public static Vector2Int UnitX => new(1, 0);

    /// <summary>
    ///     Вектор (0, 1).
    /// </summary>
    public static Vector2Int UnitY => new(0, 1);

    /// <summary>
    ///     Вектор (1, 1).
    /// </summary>
    public static Vector2Int One => new(1, 1);

    /// <summary>
    ///     Правый вектор поля.
    /// </summary>
    public static Vector2Int Right => UnitX;

    /// <summary>
    ///     Левый вектор поля.
    /// </summary>
    public static Vector2Int Left => -Right;

    /// <summary>
    ///     Верхний вектор поля.
    /// </summary>
    public static Vector2Int Up => -UnitY;

    /// <summary>
    ///     Нижний вектор поля.
    /// </summary>
    public static Vector2Int Down => UnitY;

    /// <summary>
    ///     Расстояние между двумя векторами.
    /// </summary>
    public static float Distance(Vector2Int v1, Vector2Int v2)
    {
        return (v2 - v1).Length;
    }

    /// <summary>
    ///     Сумма векторов.
    /// </summary>
    public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
    {
        return new(v1.X + v2.X, v1.Y + v2.Y);
    }

    /// <summary>
    ///     Разность векторов.
    /// </summary>
    public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
    {
        return v1 + -v2;
    }

    /// <summary>
    ///     Отрицание вектора.
    /// </summary>
    public static Vector2Int operator -(Vector2Int v)
    {
        return new(-v.X, -v.Y);
    }

    /// <summary>
    ///     Умножение на скаляр.
    /// </summary>
    public static Vector2Int operator *(Vector2Int v, int c)
    {
        return new(c * v.X, c * v.Y);
    }

    /// <summary>
    ///     Умножение на дробное число и каст в Vector2Int.
    /// </summary>
    public static Vector2Int operator *(Vector2Int v, float c)
    {
        return new((int)(c * v.X), (int)(c * v.Y));
    }

    /// <summary>
    ///     Умножение на скаляр.
    /// </summary>
    public static Vector2Int operator *(int c, Vector2Int v)
    {
        return v * c;
    }

    /// <summary>
    ///     Умножение векторов покомпонентно.
    /// </summary>
    public static Vector2Int operator *(Vector2Int v1, Vector2Int v2)
    {
        return new(v1.X * v2.X, v1.Y * v2.Y);
    }

    /// <summary>
    ///     Умножение векторов покомпонентно.
    /// </summary>
    public static Vector2Int operator *(Vector2Int v1, Vector2 v2)
    {
        return new((int)(v1.X * v2.X), (int)(v1.Y * v2.Y));
    }

    /// <summary>
    ///     Целочисленное деление вектора на скаляр.
    /// </summary>
    public static Vector2Int operator /(Vector2Int v, int c)
    {
        return new(v.X / c, v.Y / c);
    }

    /// <summary>
    ///     Каст в Vector2.
    /// </summary>
    public static implicit operator Vector2(Vector2Int v)
    {
        return new(v.X, v.Y);
    }

    /// <summary>
    ///     Каст в Point.
    /// </summary>
    public static implicit operator Point(Vector2Int v)
    {
        return new(v.X, v.Y);
    }

    /// <summary>
    ///     Каст из Vector2.
    /// </summary>
    public static implicit operator Vector2Int(Vector2 v)
    {
        return new((int)v.X, (int)v.Y);
    }
}