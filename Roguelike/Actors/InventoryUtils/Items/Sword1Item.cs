﻿namespace Roguelike.Actors.InventoryUtils.Items;

public class Sword1Item : WeaponItem
{
    public override string Name => "Great sword";

    public override string TextureName => "Sword";

    public override bool IsSword => true;

    public override float Damage => 10;
}