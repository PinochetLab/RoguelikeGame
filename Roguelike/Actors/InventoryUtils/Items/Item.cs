using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors.InventoryUtils.Items;

public abstract class Item
{
    public abstract string Name { get; }
    public abstract string TextureName { get; }
}
