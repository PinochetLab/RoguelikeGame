using Roguelike.Core;

namespace Roguelike.World;

public interface IWorldBuilder
{
    IWorldBuilder SetupSource(IWorldSource source);
    IWorldBuilder SetupWorldObjects(IObjectsProvider provider);
    // IWorldBuilder SetupEnemies(IEnemiesProvider provider);
    BaseWorldComponent Build(BaseGame game);
}