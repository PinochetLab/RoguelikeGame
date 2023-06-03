using Microsoft.Xna.Framework.Graphics;
using Roguelike.Field;
using Roguelike.VectorUtility;

namespace Roguelike.Inventory
{
    class InventoryItemComponent : Component
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch = RoguelikeGame.instance.SpriteBatch;

        public InventoryItemComponent(Actor actor) : base(actor) { }

        public void LoadTexture(string textureName)
        {
            texture = RoguelikeGame.instance.LoadTexture(textureName);
        }


        public int Weight { get; set; }
        public Vector2Int Position { get; set; }




        public void Draw()
        {
            //Vector2Int position = Transform.Position * FieldInfo.CellSize;
            //var effect = SpriteEffects.None;
            //if (FlipX) effect |= SpriteEffects.FlipHorizontally;
            //if (FlipY) effect |= SpriteEffects.FlipVertically;
            //var rect = new Rectangle(position.X, position.Y, (int)(FieldInfo.CellSize * Transform.Scale.X), (int)(FieldInfo.CellSize * Transform.Scale.Y));
            //var rectSize = new Rectangle(0, 0, texture.Width, texture.Height);
            //spriteBatch.Draw(texture, rect, rectSize, Color.WhiteSmoke);
        }
    }
}