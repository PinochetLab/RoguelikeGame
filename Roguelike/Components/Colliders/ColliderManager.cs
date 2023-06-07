using System.Collections.Concurrent;
using Roguelike.Actors;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Core;
using IUpdateable = Roguelike.Core.IUpdateable;

namespace Roguelike.Components.Colliders;

/// <summary>
/// Данный статический класс хранит информации о коллайдерах на поле.
/// </summary>
public class ColliderManager : IUpdateable
{
    /// <summary>
    /// Данный словарь хранит пары из клетки на поле и списка коллайдеров, которые в ней находятся.
    /// </summary>
    public IDictionary<Vector2Int, List<ColliderComponent>> ColliderMap { get; }
        = new ConcurrentDictionary<Vector2Int, List<ColliderComponent>>();

    private readonly List<KeyValuePair<Vector2Int, Actor>> forRemove = new ();

    /// <summary>
    /// Данный метод проверяет, есть ли в клетке v хотя бы один твёрдый коллайдер.
    /// </summary>
    public bool ContainsSolid(Vector2Int v) =>
        ColliderMap.TryGetValue(v, out var g) && g.Any(x => x.Type == ColliderType.Solid);

    /// <summary>
    /// Данный метод проверяет, есть ли в клетке игровой объект определённого типа.
    /// </summary>
    public static bool Contains<T>(Vector2Int v)
    {
        if (ColliderMap.TryGetValue(v, out var g))
        {
            return g.Any(x => x.Owner is T);
        }
        return false;
    }

    /// <summary>
    /// Данный метод удаляет коллайдер игрового объекта с поля, если он у него существует.
    /// </summary>
    public void Remove(Vector2Int v, Actor actor)
    {
        forRemove.Add(new KeyValuePair<Vector2Int, Actor>(v, actor));
    }

    public void Update(float delta)
    {
        foreach (var kv in forRemove)
            if (ColliderMap.TryGetValue(kv.Key, out var g))
                g.RemoveAll(x => x.Owner == kv.Value);

        forRemove.Clear();
    }
}
