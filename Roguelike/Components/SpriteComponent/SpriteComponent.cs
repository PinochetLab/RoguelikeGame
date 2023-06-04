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

namespace Roguelike
{
    public class SpriteComponent : Component
    {

        private Texture2D texture = null;
        private SpriteBatch spriteBatch = RoguelikeGame.instance.SpriteBatch;

        public bool FlipX { get; set; } = false;
        public bool FlipY { get; set; } = false;

        public SpriteComponent(Actor actor) : base(actor) { }

        public void LoadTexture(string textureName)
        {
            texture = TextureLoader.LoadTexture(textureName);
        }

        public void Draw()
        {
            if (texture == null) return;
            Vector2Int position = Transform.Position * FieldInfo.CellSize;
            var effect = SpriteEffects.None;
            if (FlipX) effect |= SpriteEffects.FlipHorizontally;
            if (FlipY) effect |= SpriteEffects.FlipVertically;
            var rect = new Rectangle(position.X, position.Y, (int)(FieldInfo.CellSize * Transform.Scale.X), (int)(FieldInfo.CellSize * Transform.Scale.Y));
            var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture, rect, rectSize, Color.White, 0, Vector2.Zero, effect, 0);
        }
    }
}
