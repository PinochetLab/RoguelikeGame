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

public class ImageComponent : Component, IDrawable
{
    private Texture2D texture = null;
    public Vector2Int Size { get; set; }

    private readonly SpriteBatch spriteBatch = RoguelikeGame.instance.SpriteBatch;

    public bool FlipX { get; set; } = false;
    public bool FlipY { get; set; } = false;

    public int DrawOrder => throw new NotImplementedException();

    public bool Visible => throw new NotImplementedException();

    public ImageComponent() { }

    public void LoadTexture(string textureName)
    {
        texture = TextureLoader.LoadTexture(textureName);
    }

    public void Draw()
    {
        if (texture == null) return;
        Vector2Int position = Transform.Position + (Size / 2 - Size / 2 * Transform.Scale);
        var effect = SpriteEffects.None;
        if (FlipX) effect |= SpriteEffects.FlipHorizontally;
        if (FlipY) effect |= SpriteEffects.FlipVertically;
        var rect = new Rectangle(position.X, position.Y, (int)(Size.X * Transform.Scale.X), (int)(Size.Y * Transform.Scale.Y));
        var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);
        spriteBatch.Draw(texture, rect, rectSize, Color.White, 0, Vector2.Zero, effect, 0);
    }
}
