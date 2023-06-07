using System;
using Roguelike.Core;

namespace Roguelike.Components.Colliders;

/// <summary>
/// Тип коллайдера.
/// Solid - твёрдый, через него нельзя пройти.
/// Trigger - через коллайдер можно пройти.
/// </summary>
public enum ColliderType { Solid, Trigger }

/// <summary>
/// Данный компонент - компонент коллайдера.
/// </summary>
public class ColliderComponent : Component, IUpdateable {

    /// <summary>
    /// Событие, вызывающееся, когда в текущей коллайдер попадает другой коллайдер.
    /// Другой коллайдер передаётся в событие в качестве параметра.
    /// </summary>
    public event Action<ColliderComponent> OnTriggerEnter = delegate { };

    /// <summary>
    /// Событие, вызывающееся, когда из текущего коллайдера выходит другой коллайдер.
    /// Другой коллайдер передаётся в событие в качестве параметра.
    /// </summary>
    public event Action<ColliderComponent> OnTriggerExit = delegate { };

    /// <summary>
    /// Тип коллайдера.
    /// </summary>
    public ColliderType Type { get; set; }

    private Vector2Int CurrentPosition { get; set; }

    public override void Initialize()
    {
        base.Initialize();

        CurrentPosition = Transform.Position;

        if (!ColliderManager.ColliderMap.ContainsKey(CurrentPosition))
        {
            ColliderManager.ColliderMap[CurrentPosition] = new();
        }

        ColliderManager.ColliderMap[CurrentPosition].Add(this);
    }

    private void UpdatePosition()
    {
        ColliderManager.Update();
        if (ColliderManager.ColliderMap.TryGetValue(CurrentPosition, out var list))
        { 
            list.Remove(this);
            foreach (var collider in list)
            {
                collider.OnTriggerExit(this);
                OnTriggerExit(collider);
            }
        }

        var position = Transform.Position;
            
        if (!ColliderManager.ColliderMap.ContainsKey(position)) {
            ColliderManager.ColliderMap[position] = new();
        }

        foreach (var collider in ColliderManager.ColliderMap[position]) {
            collider.OnTriggerEnter(this);
            OnTriggerEnter(collider);
        }

        ColliderManager.ColliderMap[position].Add(this);

        CurrentPosition = position;
    }

    public void Update(float delta)
    {
        if (Transform.Position == CurrentPosition) return;
        UpdatePosition();
        CurrentPosition = Transform.Position;
    }
}
