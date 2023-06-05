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

public class SpriteComponent : Component, IDrawable
{
    private Texture2D texture = null;
    public Vector2Int Size { get; set; } = Vector2Int.One;

    public bool IsTile { get; set; } = true;

    private readonly SpriteBatch spriteBatch = RoguelikeGame.Instance.SpriteBatch;

    public bool FlipX { get; set; } = false;
    public bool FlipY { get; set; } = false;

    public int DrawOrder => throw new NotImplementedException();

    public bool Visible => throw new NotImplementedException();

    public void LoadTexture(string textureName)
    {
        texture = TextureLoader.LoadTexture(textureName);
    }

    public void Draw()
    {
        if (texture == null) return;

        var pos = Transform.Position * (IsTile ? FieldInfo.CellSize : 1);
        var size = IsTile ? Vector2Int.One * FieldInfo.CellSize : Size;

        var position = pos + (size / 2 - size / 2 * Transform.Scale);

        var effect = SpriteEffects.None;
        if (FlipX) effect |= SpriteEffects.FlipHorizontally;
        if (FlipY) effect |= SpriteEffects.FlipVertically;

        var rect = new Rectangle(position.X, position.Y, (int)(size.X * Transform.Scale.X), (int)(size.Y * Transform.Scale.Y));

        var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);

        spriteBatch.Draw(texture, rect, rectSize, Color.White, 0, Vector2.Zero, effect, 0);
    }
}