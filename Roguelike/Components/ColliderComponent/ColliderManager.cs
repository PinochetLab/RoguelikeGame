using Roguelike.VectorUtility;
using Roguelike.Actors;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Components.Colliders;

public static class ColliderManager
{
    public static Dictionary<Vector2Int, List<ColliderComponent>> ColliderMap { get; set; } = new();

    private static readonly List<KeyValuePair<Vector2Int, Actor>> ForRemove = new ();

    public static bool ContainsSolid(Vector2Int v)
    {
        if (ColliderMap.TryGetValue(v, out var g))
        {
            return g.Any(x => x.Type == ColliderType.Solid);
        }
        return false;
    }

    public static void Remove(Vector2Int v, Actor actor)
    {
        ForRemove.Add(new KeyValuePair<Vector2Int, Actor>(v, actor));
    }

    public static void Update()
    {
        foreach (var kv in ForRemove)
        {
            if (ColliderMap.TryGetValue(kv.Key, out var g))
            {
                g.RemoveAll(x => x.Owner == kv.Value);
            }
        }
        ForRemove.Clear();
    }
}
