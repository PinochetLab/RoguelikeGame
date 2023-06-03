using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Components.ColliderComponent
{
    public static class ColliderManager
    {
        public static Dictionary<Vector2Int, List<ColliderComponent>> ColliderMap { get; set; } = new();

        public static bool ContainsSolid(Vector2Int v)
        {
            if (ColliderMap.TryGetValue(v, out var g))
            {
                return g.Any(x => x.Type == ColliderType.Solid);
            }
            return false;
        }
    }
}
