using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguelike.Core;

namespace Roguelike.Components;

/// <summary>
///     Данный класс отвечает за текст на экране
/// </summary>
public class TextComponent : DrawableComponent
{
    private SpriteFont spriteFont;
    public override int DrawOrder { get; set; } = 0;

    /// <summary>
    ///     Текст
    /// </summary>
    public string Text { get; set; } = "Собака и кега";

    /// <summary>
    ///     Установить шрифт
    /// </summary>
    public void SetSpriteFont(string path)
    {
        spriteFont = Owner.Game.GetSpriteFont(path);
    }

    public override void Draw(GameTime time, SpriteBatch batch)
    {
        if (spriteFont == null) return;
        batch.DrawString(spriteFont, Text, Transform.Position, Color.White);
    }
}