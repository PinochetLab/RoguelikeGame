using System.Collections.Generic;
using Roguelike.Actors.InventoryUtils.Items.Attacks;

namespace Roguelike.Actors.InventoryUtils.Items;

public abstract class WeaponItem : Item
{
    public abstract override string Name { get; }
    public abstract override string TextureName { get; }
    public List<IAttack> Attacks { get; set; } = new();
}