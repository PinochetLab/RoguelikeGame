using System;
using System.Collections.Generic;
using Roguelike.Actors;
using Roguelike.Core;
using Roguelike.World.Providers;

namespace Roguelike.World;

public class DefaultObjectsProvider : IObjectsProvider
{
    private readonly Dictionary<AttributeType, Func<WorldComponent, Vector2Int, Actor>> builders = new()
    {
        [AttributeType.Loot] = (component, coords) => component.CreateActor<Box>(coords)
    };

    public Actor CreateActorInScene(WorldComponent component, Tile info, Vector2Int coords)
    {
        if (builders.TryGetValue(info.Attributes, out var builder))
            return builder.Invoke(component, coords);

        switch (info.MaterialType)
        {
            case MaterialType.Floor:
                return null;
            case MaterialType.Air:
            case MaterialType.Wall:
                return component.CreateActor<Wall>(coords);
            default:
                throw new ArgumentException("Can't create actor with undefined type");
        }
    }
}