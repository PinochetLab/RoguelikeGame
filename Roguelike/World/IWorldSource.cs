using Roguelike.World.Providers;

namespace Roguelike.World;

public interface IWorldSource
{
    ITileMap GetMapRepresentation();
}