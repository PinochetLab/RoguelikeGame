using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Roguelike.Actors;
using Roguelike.Field;

namespace Roguelike.World;

public class WorldComponent : DrawableGameComponent
{
    private SpriteBatch spriteBatch;

    private const string FloorTextureName = "GrassTile";
    private Texture2D floorTexture;

    public WorldComponent(Game game) : base(game)
    { }

    public override void Initialize()
    {
        base.Initialize();
        Actor.Create<ItemHolder>(7, 3);

        Actor.Create<Hero>(FieldInfo.Center);


        for (var i = 0; i < FieldInfo.CellCount; i++)
        {
            Actor.Create<Wall>(0, i);
            Actor.Create<Wall>(FieldInfo.CellCount - 1, i);

            if (i <= 0 || i >= FieldInfo.CellCount - 1) continue;

            Actor.Create<Wall>(i, 0);
            Actor.Create<Wall>(i, FieldInfo.CellCount - 1);
        }
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        floorTexture = Game.Content.Load<Texture2D>(FloorTextureName);
    }

    public override void Draw(GameTime gameTime)
    {
        var size = FieldInfo.ScreenWith;
        var cellCount = FieldInfo.CellCount;
        var cellSize = FieldInfo.CellSize;

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        for (var i = 0; i < cellCount; i++)
        for (var j = 0; j < cellCount; j++)
        {
            var tex = floorTexture;
            var rectangle = new Rectangle(i * cellSize, j * cellSize, 1 * cellSize, 1 * cellSize);
            var rectSize = new Rectangle(0, 0, tex.Width, tex.Height);
            spriteBatch.Draw(tex, rectangle, rectSize, Color.WhiteSmoke);
        }

        for (var i = 1; i < cellCount; i++)
        {
            spriteBatch.DrawLine(new Vector2(0, i * cellSize), new Vector2(size, i * cellSize), Color.Black, 2);
            spriteBatch.DrawLine(new Vector2(i * cellSize, 0), new Vector2(i * cellSize, size), Color.Black, 2);
        }

        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}