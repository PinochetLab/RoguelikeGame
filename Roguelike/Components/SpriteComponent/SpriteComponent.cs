using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Roguelike.Core;
using Roguelike.Field;
using IDrawable = Roguelike.Core.IDrawable;

namespace Roguelike.Components.Sprites;

/// <summary>
/// Данный компонент отвечает за отрисовку спрайта.
/// </summary>
public class SpriteComponent : Component, IDrawable
{
    private Texture2D texture = null;

    /// <summary>
    /// Размер прямоугольника для отрисовки в пикселях.
    /// </summary>
    public Vector2Int Size { get; set; } = Vector2Int.One;

    /// <summary>
    /// Если данное поле истино, объект является объектом на тайловом поле, его спрайт будет иметь позицию соответствующей клетки,
    /// а размер будет браться за размер клетки.
    /// </summary>
    public bool IsTile { get; set; } = true;

    /// <summary>
    /// Отзеркаливание по горизонтали.
    /// </summary>
    public bool FlipX { get; set; } = false;

    /// <summary>
    /// Отзеркаливание по вертикали.
    /// </summary>
    public bool FlipY { get; set; } = false;

    /// <summary>
    /// Видимость.
    /// </summary>
    public bool Visible { get; set; } = true;
    public bool Canvas { get; set; } = false;

    public int DrawOrder { get; set; } = 0;

    /// <summary>
    /// Данный метод устанавливает текстуру в текстуру, соответствующую названию.
    /// </summary>
    public void SetTexture(string textureName)
    {
        texture = Owner.Game.GetTexture(textureName);
    }

    public void Draw(GameTime time, SpriteBatch batch)
    {
        if (!Visible) return;
        if (texture == null) return;

        var pos = Transform.Position * (IsTile ? FieldInfo.CellSize : 1);
        var size = IsTile ? Vector2Int.One * FieldInfo.CellSize : Size;

        var position = pos + size / 2;

        var effect = SpriteEffects.None;
        if (FlipX) effect |= SpriteEffects.FlipHorizontally;
        if (FlipY) effect |= SpriteEffects.FlipVertically;

        var rect = new Rectangle(position.X, position.Y, (int)(size.X * Transform.Scale.X), (int)(size.Y * Transform.Scale.Y));

        if (rect.Width < 0)
        {
            rect.X += rect.Width;
            rect.Width = -rect.Width;
            effect |= SpriteEffects.FlipHorizontally;
        }

        if (rect.Height < 0)
        {
            rect.Y += rect.Height;
            rect.Height = -rect.Height;
            effect |= SpriteEffects.FlipVertically;
        }

        var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);

        batch.Draw(texture, rect, rectSize, Color.White, Transform.Angle, new Vector2(texture.Width / 2f, texture.Height / 2f), effect, 0);
    }
}