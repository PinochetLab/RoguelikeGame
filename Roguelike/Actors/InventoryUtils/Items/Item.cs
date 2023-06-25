namespace Roguelike.Actors.InventoryUtils.Items;

/// <summary>
///     Данный класс отвечает за предметы.
/// </summary>
public abstract class Item
{
    public abstract string Name { get; }
    public abstract string TextureName { get; }
}