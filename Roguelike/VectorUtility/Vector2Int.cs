using System;
using Microsoft.Xna.Framework;

namespace Roguelike.VectorUtility;

public record struct Vector2Int
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public float Length => MathF.Sqrt(X * X + Y * Y);

    public static float Distance(Vector2Int v1, Vector2Int v2) => (v2 - v1).Length;

    public static Vector2Int operator +(Vector2Int v1, Vector2Int v2) => new(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2Int operator -(Vector2Int v1, Vector2Int v2) => v1 + (-v2);
    public static Vector2Int operator -(Vector2Int v) => new(-v.X, -v.Y);
    public static Vector2Int operator *(Vector2Int v, int c) => new(c * v.X, c * v.Y);
    public static Vector2Int operator *(Vector2Int v, float c) => new((int)(c * v.X), (int)(c * v.Y));
    public static Vector2Int operator *(int c, Vector2Int v) => v * c;
    public static Vector2Int operator *(Vector2Int v1, Vector2Int v2) => new (v1.X * v2.X, v1.Y * v2.Y);
    public static Vector2Int operator *(Vector2Int v1, Vector2 v2) => new ((int)(v1.X * v2.X), (int)(v1.Y * v2.Y));
    public static Vector2Int operator /(Vector2Int v, int c) => new(v.X / c, v.Y / c);


    public static implicit operator Vector2(Vector2Int v) => new(v.X, v.Y);
    public static implicit operator Point(Vector2Int v) => new(v.X, v.Y);

    public static implicit operator Vector2Int(Vector2 v) => new((int)v.X, (int)v.Y);

    public static Vector2Int Zero => new(0, 0);
    public static Vector2Int UnitX => new(1, 0);
    public static Vector2Int UnitY => new(0, 1);
    public static Vector2Int One => new(1, 1);

    public static Vector2Int Right => UnitX;
    public static Vector2Int Left => -Right;
    public static Vector2Int Up => -UnitY;
    public static Vector2Int Down => UnitY;
}

