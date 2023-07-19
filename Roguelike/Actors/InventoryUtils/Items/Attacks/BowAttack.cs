using System;
using System.Collections.Generic;
using Roguelike.Components;
using Roguelike.Components.AttackModifiers;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

/// <summary>
///     Данный класс отвечает за атаку луком.
/// </summary>
public class BowAttack<TAmmo> : IRangeAttack where TAmmo : Actor, ICloneable
{
    /// <summary>
    ///     Прототип снаряда
    /// </summary>
    public TAmmo Arrow { get; set; }

    /// <summary>
    ///     Дистанция атаки
    /// </summary>
    public List<Vector2Int> Range { get; set; } = new();

    public void Attack(Actor actor, Direction direction)
    {
        var ammo = Arrow.Clone() as Actor;

        var modifier = actor.GetComponent<AttackModifierComponent>();
        var damager = ammo.GetComponent<DamagerComponent>();

        if (modifier != null && damager != null)
            foreach (var damage in damager.Damages)
                damager.Damages[damage.Key] = modifier.Apply(damage.Value);

        ammo.Transform.Position = actor.Transform.Position;
        ammo.Transform.Direction = direction;
    }
}