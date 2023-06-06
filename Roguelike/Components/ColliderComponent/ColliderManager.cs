using Roguelike.VectorUtility;
using Roguelike.Actors;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Components.Colliders;

/// <summary>
/// Данный статический класс хранит информации о коллайдерах на поле.
/// </summary>
public static class ColliderManager
{
    /// <summary>
    /// Данный словарь хранит пары из клетки на поле и списка коллайдеров, которые в ней находятся.
    /// </summary>
    public static Dictionary<Vector2Int, List<ColliderComponent>> ColliderMap { get; set; } = new();

    private static readonly List<KeyValuePair<Vector2Int, Actor>> ForRemove = new ();

    /// <summary>
    /// Данный метод проверяет, есть ли в клетке v хотя бы один твёрдый коллайдер.
    /// </summary>
    public static bool ContainsSolid(Vector2Int v)
    {
        if (ColliderMap.TryGetValue(v, out var g))
        {
            return g.Any(x => x.Type == ColliderType.Solid);
        }
        return false;
    }

    /// <summary>
    /// Данный метод удаляет коллайдер игрового объекта с поля, если он у него существует.
    /// </summary>
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
