using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Roguelike.Core;
using Roguelike.Field;
using IDrawable = Roguelike.Core.IDrawable;
using System.Dynamic;
using System.Diagnostics;

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
    /// Дополнительное масштабирование.
    /// </summary>
    public Vector2 AdditionalScale { get; set; } = Vector2.One;

    /// <summary>
    /// Отзеркаливание по горизонтали.
    /// </summary>
    public bool FlipX { get; set; } = false;

    /// <summary>
    /// Отзеркаливание по вертикали.
    /// </summary>
    public bool FlipY { get; set; } = false;

    public Vector2Int Offset { get; set; } = Vector2Int.Zero;

    /// <summary>
    /// Центр спрайта относительно повророта и масштабирования.
    /// </summary>
    public Vector2 Pivot { get; set; } = Vector2.One / 2;

    /// <summary>
    /// Цвет спрайта.
    /// </summary>
    public Color Color { get; set; } = Color.White;

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

        Vector2 scale = AdditionalScale * Transform.Scale;

        var pos = Transform.Position * (Transform.IsTile ? FieldInfo.CellSize : 1);
        var size = Transform.IsTile ? Vector2Int.One * FieldInfo.CellSize : Size;

        var position = pos + (Transform.IsTile ? size * Pivot : Vector2Int.Zero);

        

        var effect = SpriteEffects.None;
        if (FlipX) effect |= SpriteEffects.FlipHorizontally;
        if (FlipY) effect |= SpriteEffects.FlipVertically;

        var rect = new Rectangle(position.X, position.Y, (int)(size.X * scale.X), (int)(size.Y * scale.Y));

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
        
        rect.X += Offset.X;
        rect.Y += Offset.Y;

        //Debug.WriteLine(texture);

        var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);

        batch.Draw(texture, rect, rectSize, Color, Transform.Angle, new Vector2(texture.Width, texture.Height) * Pivot, effect, 0);
    }
}