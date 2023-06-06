using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Roguelike.Field;
using Roguelike.VectorUtility;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Roguelike.Texture;

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

    private static SpriteBatch SpriteBatch => RoguelikeGame.Instance.SpriteBatch;

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
        texture = TextureMaster.GetTexture(textureName);
    }

    public void Draw(float delta)
    {
        if (!Visible) return;
        if (texture == null) return;

        var pos = Transform.Position * (IsTile ? FieldInfo.CellSize : 1);
        var size = IsTile ? Vector2Int.One * FieldInfo.CellSize : Size;

        var position = pos + (size / 2 - size / 2 * Transform.Scale);

        var effect = SpriteEffects.None;
        if (FlipX) effect |= SpriteEffects.FlipHorizontally;
        if (FlipY) effect |= SpriteEffects.FlipVertically;

        var rect = new Rectangle(position.X, position.Y, (int)(size.X * Transform.Scale.X), (int)(size.Y * Transform.Scale.Y));

        var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);

        SpriteBatch.Draw(texture, rect, rectSize, Color.White, 0, Vector2.Zero, effect, 0);
    }
}