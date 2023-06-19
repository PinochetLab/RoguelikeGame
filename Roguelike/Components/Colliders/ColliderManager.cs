using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Components.Colliders;

/// <summary>
///     Данный статический класс хранит информации о коллайдерах на поле.
/// </summary>
public class ColliderManager : IUpdateable
{
    private readonly List<KeyValuePair<Vector2Int, Actor>> forRemove = new();

    /// <summary>
    ///     Данный словарь хранит пары из клетки на поле и списка коллайдеров, которые в ней находятся.
    /// </summary>
    public IDictionary<Vector2Int, List<ColliderComponent>> ColliderMap { get; }
        = new ConcurrentDictionary<Vector2Int, List<ColliderComponent>>();

    public void Update(float delta)
    {
        foreach (var kv in forRemove)
            if (ColliderMap.TryGetValue(kv.Key, out var g))
                g.RemoveAll(x => x.Owner == kv.Value);

        forRemove.Clear();
    }

    /// <summary>
    ///     Данный метод проверяет, есть ли в клетке v хотя бы один твёрдый коллайдер.
    /// </summary>
    public bool ContainsSolid(Vector2Int v)
    {
        return ColliderMap.TryGetValue(v, out var g) && g.Any(x => x.Type == ColliderType.Solid);
    }

    /// <summary>
    ///     Данный метод проверяет, есть ли в клетке игровой объект определённого типа.
    /// </summary>
    public bool Contains<T>(Vector2Int v)
    {
        return ColliderMap.TryGetValue(v, out var g) && g.Any(x => x.Owner is T);
    }

    /// <summary>
    ///     Данный метод возвращает первый объект необходимого типа в клетке, если такой присутствует, и null в противном
    ///     случае.
    /// </summary>
    public T Find<T>(Vector2Int v) where T : class
    {
        if (ColliderMap.TryGetValue(v, out var g)) return g.Find(x => x?.Owner is T)?.Owner as T;
        return null;
    }

    /// <summary>
    ///     Данный метод возвращает все объекты необходимого типа в клетке, если такой присутствует, и null в противном случае.
    /// </summary>
    public IEnumerable<T> FindAll<T>(Vector2Int v) where T : class
    {
        if (ColliderMap.TryGetValue(v, out var g)) return g.FindAll(x => x?.Owner is T).Select(x => x.Owner as T);
        return new List<T>();
    }

    /// <summary>
    ///     Данный метод удаляет коллайдер игрового объекта с поля, если он у него существует.
    /// </summary>
    public void Remove(Vector2Int v, Actor actor)
    {
        forRemove.Add(new KeyValuePair<Vector2Int, Actor>(v, actor));
    }
}