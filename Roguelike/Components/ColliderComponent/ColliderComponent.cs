using System;
using Roguelike.VectorUtility;

namespace Roguelike.Components.Colliders;

public enum ColliderType { Solid, Trigger }

public class ColliderComponent : Component {

    public event Action<ColliderComponent> OnTriggerEnter = delegate { };
    public event Action<ColliderComponent> OnTriggerExit = delegate { };

    public ColliderType Type { get; set; }

    public Vector2Int CurrentPosition { get; set; }
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

    public void UpdatePosition()
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
}
