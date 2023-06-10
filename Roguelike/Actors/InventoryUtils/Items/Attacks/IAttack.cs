using System.Collections.Generic;
using System.Threading.Tasks;
using Roguelike.Components;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public interface IAttack
{
    void Atack(Actor actor,  Direction direction);
    void Animation(Actor actor){}
    public List<Vector2Int> range { get; set; }
}