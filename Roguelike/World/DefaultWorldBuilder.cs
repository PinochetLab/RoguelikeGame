using System;
using System.Linq;
using Roguelike.Actors.Enemies;
using Roguelike.Core;
using Roguelike.World.Providers;

namespace Roguelike.World;

public class DefaultWorldBuilder : IWorldBuilder
{
    private IObjectsProvider objectsProvider;
    private IWorldSource worldSource;

    public IWorldBuilder SetupSource(IWorldSource source)
    {
        worldSource = source;
        return this;
    }

    public IWorldBuilder SetupWorldObjects(IObjectsProvider provider)
    {
        objectsProvider = provider;
        return this;
    }

    public BaseWorldComponent Build(BaseGame game)
    {
        var template = worldSource.GetMapRepresentation();

        var entry = template.First(x => x.tile.Attributes.HasFlag(AttributeType.Entry));

        var component = new WorldComponent(game, new Vector2Int(entry.x, entry.y));
        game.World = component;

        foreach (var (x, y, tile) in template)
        {
            var type = tile.Attributes;
            if (type.HasFlag(AttributeType.MobSpawn))
            {
                //TODO: Add mobs spawn provider
                if (Random.Shared.Next(0, 100) > 25)
                    component.CreateActor<Froggy>(x, y);
                else
                    component.CreateActor<Frogger>(x, y);
            }
            else
            {
                objectsProvider.CreateActorInScene(component, tile, new Vector2Int(x, y));
            }
        }

        component.Initialize();
        return component;
    }
}