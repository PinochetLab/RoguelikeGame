using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Roguelike.Actors;
using Roguelike.Actors.AI;
using Roguelike.Core;
using Roguelike.Field;

namespace Roguelike.World;

public class WorldComponent : BaseWorldComponent
{
    private SpriteBatch spriteBatch;

    private const string FloorTextureName = "GrassTile";
    private Texture2D floorTexture;

    public WorldComponent(BaseGame game) : base(game)
    { }

    public override void Initialize()
    {
        base.Initialize();
        CreateActor<ItemHolder>(7, 3);

        CreateActor<Hero>(FieldInfo.Center);

        CreateActor<Enemy>(11, 2);


        for (var i = 0; i < FieldInfo.CellCount; i++)
        {
            CreateActor<Wall>(0, i);
            CreateActor<Wall>(FieldInfo.CellCount - 1, i);

            if (i <= 0 || i >= FieldInfo.CellCount - 1) continue;

            CreateActor<Wall>(i, 0);
            CreateActor<Wall>(i, FieldInfo.CellCount - 1);
        }
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        floorTexture = Game.Content.Load<Texture2D>(FloorTextureName);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

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
}