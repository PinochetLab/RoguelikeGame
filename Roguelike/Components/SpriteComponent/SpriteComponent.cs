using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Roguelike
{
    public class SpriteComponent : Component
    {

        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private bool flipX = true;
        private bool flipY = false;

        public bool FlipX { get => flipX; set => flipX = value; }
        public bool FlipY { get => flipY; set => flipY = value; }

        public SpriteComponent(string path)
        {
            texture = RoguelikeGame.instance.LoadTexture(path);
            spriteBatch = RoguelikeGame.instance.SpriteBatch;
        }

        public void Draw(Vector2 position)
        {
            SpriteEffects effect = SpriteEffects.None;
            if (flipX) effect |= SpriteEffects.FlipHorizontally;
            if (flipY) effect |= SpriteEffects.FlipVertically;
            Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0, Vector2.One, effect, 0);
        }
    }
}
