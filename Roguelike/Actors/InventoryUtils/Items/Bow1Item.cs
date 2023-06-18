namespace Roguelike.Actors.InventoryUtils.Items;

public class Bow1Item : WeaponItem
{
    public override string Name => "Great sword";

    public override string TextureName => "Bow3";

    public override bool IsSword => false;

    public override float Damage => 10;
}