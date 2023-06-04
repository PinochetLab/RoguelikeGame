using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors.InventoryUtils.Items
{
    public class ItemCoin : Item
    {
        public override string Name { get => "Монеточка"; }
        public override string TextureName { get => "Coin"; }
    }
}
