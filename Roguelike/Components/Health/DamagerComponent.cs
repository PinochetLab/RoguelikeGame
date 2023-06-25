using System.Collections.Generic;
using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Components;

public class DamagerComponent : Component
{
    public Dictionary<Vector2Int, int> Damages { get; set; }

    public void Damage()
    {
        foreach (var (place, damage) in Damages)
        foreach (var damageable in Owner.World.Colliders.FindAll<IDamageable>(Owner.Transform.Position +
                                                                              place.Rotate(Owner.Transform.Direction)))
            if (damageable != Owner)
                damageable.TakeDamage(damage);
    }
}