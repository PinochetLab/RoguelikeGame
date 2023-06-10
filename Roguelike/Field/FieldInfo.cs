using Roguelike.Core;

namespace Roguelike.Field;

/// <summary>
/// Класс, предоставляющий информацию о поле.
/// </summary>
public static class FieldInfo {

    /// <summary>
    /// Ширина экрана в пикселях.
    /// </summary>
    public static int ScreenWith { get; set; } = 750;

    /// <summary>
    /// Высота экрана в пикселях.
    /// </summary>
    public static int ScreenHeight { get; set; } = 850;

    /// <summary>
    /// Ширина поля в клетках (поле - квадратное).
    /// </summary>
    public static int CellCount { get; set; } = 15;

    /// <summary>
    /// Размер клетки.
    /// </summary>
    public static int CellSize => ScreenWith / CellCount;

    /// <summary>
    /// Размер клетки.
    /// </summary>
    public static Vector2Int CellSizeVector => new (CellSize, CellSize);

    /// <summary>
    /// Центер поля.
    /// </summary>
    public static Vector2Int Center => Vector2Int.One * (CellCount / 2);

    /// <summary>
    /// Левый верхний угол поля.
    /// </summary>
    public static Vector2Int TopLeft => new (0, 0);

    /// <summary>
    /// Правый верхний угол поля.
    /// </summary>
    public static Vector2Int TopRight => new (0, CellCount - 1);

    /// <summary>
    /// Левый нижний угол поля.
    /// </summary>
    public static Vector2Int BottomLeft => new (CellCount - 1, 0);

    /// <summary>
    /// Правый нижний угол поля.
    /// </summary>
    public static Vector2Int BottomRight => new (CellCount - 1, CellCount - 1);
}
