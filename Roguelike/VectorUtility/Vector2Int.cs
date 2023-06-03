using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Roguelike.VectorUtility {
    public record struct Vector2Int {
        public int X;
        public int Y;

        public Vector2Int(int X, int Y) {
            this.X = X;
            this.Y = Y;
        }

        public float Length {
            get => MathF.Sqrt(X * X + Y * Y);
        }

        public static float Distance(Vector2Int v1, Vector2Int v2) {
            return (v2 - v1).Length;
        }

        #region OPERATOR_OVERLOAD

        public static Vector2Int operator +(Vector2Int v1, Vector2Int v2) 
        {
            return new Vector2Int(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2Int operator -(Vector2Int v1, Vector2Int v2) 
        {
            return new Vector2Int(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2Int operator -(Vector2Int v)
        {
            return new Vector2Int(-v.X, -v.Y);
        }

        public static Vector2Int operator *(Vector2Int v1, int c) {
            return new Vector2Int(c * v1.X, c * v1.Y);
        }

        public static Vector2Int operator *(int c, Vector2Int v1) {
            return v1 * c;
        }

        #endregion

        public static implicit operator Vector2(Vector2Int v) {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator Vector2Int(Vector2 v) {
            return new Vector2Int((int)v.X, (int)v.Y);
        }


        public static Vector2Int Zero {
            get => new Vector2Int(0, 0);
        }

        public static Vector2Int UnitX {
            get => new Vector2Int(1, 0);
        }

        public static Vector2Int UnitY {
            get => new Vector2Int(0, 1);
        }

        public static Vector2Int One {
            get => new Vector2Int(1, 1);
        }

        public static Vector2Int Right { get => UnitX; }
        public static Vector2Int Left { get => -Right; }
        public static Vector2Int Up { get => -UnitY; }
        public static Vector2Int Down { get => UnitY; }
    }
}
