using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors.InventoryUtils.Items;
public abstract class WeaponItem : Item
{
    public abstract override string Name { get; }
    public abstract override string TextureName { get; }

    public abstract bool IsSword { get; }

    public abstract float Damage { get; }
}
