using System;
using System.Collections.Generic;
using Roguelike.Components;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public class BowAttack<TAmmo>:IRangeAttack where TAmmo: Actor, ICloneable
{
    public List<Vector2Int> range { get; set; } = new();
    public TAmmo Arrow { get; set; }
    public void Atack(Actor actor, Direction direction)
    {
        var ammo = Arrow.Clone() as Actor;
        ammo.Transform.Position = actor.Transform.Position + direction;
        ammo.Transform.Direction = direction;
    }
}