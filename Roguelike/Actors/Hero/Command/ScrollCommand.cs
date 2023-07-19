using Roguelike.Actors.InventoryUtils;

namespace Roguelike.Commands;

/// <summary>
///     Данный класс отвечает за команду игрока поменять текущий предмет в инвентаре
/// </summary>
public class ScrollCommand : ICommand
{
    private readonly int count;

    private readonly Inventory inventory;

    public ScrollCommand(Inventory inventory, int count)
    {
        this.inventory = inventory;
        this.count = count;
    }

    public void Execute()
    {
        inventory.Scroll(count);
    }
}