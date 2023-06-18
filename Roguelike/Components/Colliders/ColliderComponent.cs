using System;
using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Components.Colliders;

/// <summary>
///     Тип коллайдера.
///     Solid - твёрдый, через него нельзя пройти.
///     Trigger - через коллайдер можно пройти.
/// </summary>
public enum ColliderType
{
    Solid,
    Trigger
}

/// <summary>
///     Данный компонент - компонент коллайдера.
/// </summary>
public class ColliderComponent : Component, IUpdateable
{
    /// <summary>
    ///     Тип коллайдера.
    /// </summary>
    public ColliderType Type { get; set; }

    private Vector2Int CurrentPosition { get; set; }

    private ColliderManager Manager => Owner.World.Colliders;

    public void Update(float delta)
    {
        if (Transform.Position == CurrentPosition) return;
        UpdatePosition();
        CurrentPosition = Transform.Position;
    }

    /// <summary>
    ///     Событие, вызывающееся, когда в текущей коллайдер попадает другой коллайдер.
    ///     Другой коллайдер передаётся в событие в качестве параметра.
    /// </summary>
    public event Action<ColliderComponent> OnTriggerEnter;

    /// <summary>
    ///     Событие, вызывающееся, когда из текущего коллайдера выходит другой коллайдер.
    ///     Другой коллайдер передаётся в событие в качестве параметра.
    /// </summary>
    public event Action<ColliderComponent> OnTriggerExit;

    public override void Initialize()
    {
        base.Initialize();

        CurrentPosition = Transform.Position;

        if (!Manager.ColliderMap.ContainsKey(CurrentPosition))
            Manager.ColliderMap[CurrentPosition] = new List<ColliderComponent>();

        Manager.ColliderMap[CurrentPosition].Add(this);
    }

    private void UpdatePosition()
    {
        if (Manager.ColliderMap.TryGetValue(CurrentPosition, out var list))
        {
            list.Remove(this);
            foreach (var collider in list)
            {
                collider.OnTriggerExit?.Invoke(this);
                OnTriggerExit?.Invoke(collider);
            }
        }

        var position = Transform.Position;

        if (!Manager.ColliderMap.ContainsKey(position))
            Manager.ColliderMap[position] = new List<ColliderComponent>();

        foreach (var collider in Manager.ColliderMap[position])
        {
            collider.OnTriggerEnter?.Invoke(this);
            OnTriggerEnter?.Invoke(collider);
        }

        Manager.ColliderMap[position].Add(this);

        CurrentPosition = position;
    }
}