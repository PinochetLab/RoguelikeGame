using Roguelike.VectorUtility;

namespace Roguelike.Field {
    public static class FieldInfo {
        public static int ScreenWith { get; set; } = 750;
        public static int ScreenHeight { get; set; } = 850;

        public static int CellCount { get; set; } = 15;

        public static int CellSize => ScreenWith / CellCount;

        public static Vector2Int Center => Vector2Int.One * (CellCount / 2);

        public static Vector2Int TopLeft => new (0, 0);
        public static Vector2Int TopRight => new (0, CellCount - 1);
        public static Vector2Int BottomLeft => new (CellCount - 1, 0);
        public static Vector2Int BottomRight => new (CellCount - 1, CellCount - 1);
    }
}
