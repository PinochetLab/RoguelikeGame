using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguelike.Core;

namespace Roguelike.Components;

public class TextComponent : DrawableComponent
{
    private SpriteFont spriteFont;
    public override int DrawOrder { get; set; } = 0;

    public string Text { get; set; } = "Собака и кега";

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