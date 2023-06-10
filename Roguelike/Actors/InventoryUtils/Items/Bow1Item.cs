﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors.InventoryUtils.Items;
public class Bow1Item : WeaponItem
{
    public override string Name => "Great sword";

    public override string TextureName => "Bow3";

    public override bool IsSword => false;

    public override int Damage => 10;
}
