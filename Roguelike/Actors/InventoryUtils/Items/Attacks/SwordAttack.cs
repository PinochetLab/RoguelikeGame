﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Roguelike.Components.AttackModifiers;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public class SwordAttack : IMeleeAttack
{
    public int Damage { get; set; }
    public List<Vector2Int> Range { get; set; } = new();

    public void Attack(Actor actor, Direction direction)
    {
        Task.Run(Animation);

        var modifier = actor.GetComponent<AttackModifierComponent>();
        var currentAttackDamage = Damage;
        if (modifier != null) currentAttackDamage = modifier.Apply(currentAttackDamage);

        foreach (var attack in Range)
        foreach (var damageable in actor.World.Colliders.FindAll<IDamageable>(actor.Transform.Position +
                                                                              attack.Rotate(direction)))
            if (damageable != actor)
                damageable.TakeDamage(currentAttackDamage);
    }

    private static async void Animation()
    {
        for (var i = 0; i < 2; i++)
        {
            Hero.Instance.WeaponSlot.Offset = Hero.Instance.CurrentDirection * 30;
            await Task.Delay(50);
            Hero.Instance.WeaponSlot.Offset = Vector2Int.Zero;
            await Task.Delay(50);
        }
    }
}