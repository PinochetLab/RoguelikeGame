using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public interface IAttack
{
    public List<Vector2Int> Range { get; set; }
    void Attack(Actor actor, Direction direction);
}