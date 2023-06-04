using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Components.ColliderComponent
{
    public static class ColliderManager
    {
        public static Dictionary<Vector2Int, List<ColliderComponent>> ColliderMap { get; set; } = new();

        public static List<KeyValuePair<Vector2Int, Actor>> forRemove = new ();

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
            forRemove.Add(new KeyValuePair<Vector2Int, Actor>(v, actor));
        }

        public static void Update()
        {
            foreach (var kv in forRemove)
            {
                if (ColliderMap.TryGetValue(kv.Key, out var g))
                {
                    g.RemoveAll(x => x.Owner == kv.Value);
                }
            }
            forRemove.Clear();
        }
    }
}
