using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public interface IAttack
{
    public List<Vector2Int> range { get; set; }
    void Atack(Actor actor, Direction direction);

    void Animation(Actor actor)
    {
    }
}