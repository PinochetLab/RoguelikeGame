namespace Roguelike.World.Providers.Generator;

public interface ILevelGenerator
{
    ITileMap Generate(int width, int height);
}