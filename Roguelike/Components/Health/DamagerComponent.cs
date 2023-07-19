using System.Collections.Generic;
using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Components;

/// <summary>
///     Данный компонент отвечает за актора, который наносит урон касанием
/// </summary>
public class DamagerComponent : Component
{
    /// <summary>
    ///     Задает хитбокс и урон в каждой его точке
    /// </summary>
    public Dictionary<Vector2Int, int> Damages { get; set; }

    /// <summary>
    ///     Вызывается при нанесении урона касанием
    /// </summary>
    public void Damage()
    {
        foreach (var (place, damage) in Damages)
        foreach (var damageable in Owner.World.Colliders.FindAll<IDamageable>(Owner.Transform.Position +
                                                                              place.Rotate(Owner.Transform.Direction)))
            if (damageable != Owner)
                damageable.TakeDamage(damage);
    }
}