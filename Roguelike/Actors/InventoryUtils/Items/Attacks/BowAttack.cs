using System;
using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public class BowAttack<TAmmo> : IRangeAttack where TAmmo : Actor, ICloneable
{
    public TAmmo Arrow { get; set; }
    public List<Vector2Int> range { get; set; } = new();

    public void Atack(Actor actor, Direction direction)
    {
        var ammo = Arrow.Clone() as Actor;
        ammo.Transform.Position = actor.Transform.Position;
        ammo.Transform.Direction = direction;
    }
}