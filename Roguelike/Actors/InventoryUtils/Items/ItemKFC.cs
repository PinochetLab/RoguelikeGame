﻿using System.Collections.Generic;
using Roguelike.Actors.InventoryUtils.Items.Attacks;

namespace Roguelike.Actors.InventoryUtils.Items;

/// <summary>
///     Курица из КФЦ, хилит при использовании
/// </summary>
public class ItemKFC : OneUseItem
{
    public ItemKFC()
    {
        Attacks = new List<IAttack> { new Heal(40) };
    }

    public override string Name => "Курочка KFC";
    public override string TextureName => "KFC";
}