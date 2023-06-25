using System.Collections.Generic;
using Roguelike.Components;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public class Heal : ISpecialAttack
{
    public Heal(int healAmount)
    {
        HealAmount = healAmount;
    }

    public int HealAmount { get; set; }
    public List<Vector2Int> Range { get; set; } = new();

    public void Attack(Actor actor, Direction direction)
    {
        var healthComponent = actor.GetComponent<HealthComponent>();
        if (healthComponent is null) return;
        healthComponent.Health += HealAmount;
    }
}