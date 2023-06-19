using Roguelike.Core;

namespace Roguelike.World;

public interface IWorldBuilder
{
    IWorldBuilder SetupSource(IWorldSource source);

    IWorldBuilder SetupWorldObjects(IObjectsProvider provider);
    BaseWorldComponent Build(BaseGame game);
}