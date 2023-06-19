using Roguelike.Actors;
using Roguelike.Core;
using Roguelike.World.Providers;

namespace Roguelike.World;

public interface IObjectsProvider
{
    Actor CreateActorInScene(WorldComponent component, Tile info, Vector2Int coords);
}