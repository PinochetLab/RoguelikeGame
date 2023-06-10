using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguelike.Core;

namespace Roguelike.Components;
public class TextComponent : DrawableComponent
{
    public override int DrawOrder { get; set; } = 0;

    private SpriteFont spriteFont;

    public void SetSpriteFont(string path)
    {
        spriteFont = Owner.Game.GetSpriteFont(path);
    }

    public string Text { get; set; } = "Собака и кега";

    public override void Draw(GameTime time, SpriteBatch batch)
    {
        if (spriteFont == null)
        {
            return;
        }
        batch.DrawString(spriteFont, Text, Transform.Position, Color.White);
    }
}
