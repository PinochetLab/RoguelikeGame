using Roguelike.Actors.InventoryUtils;

namespace Roguelike.Commands;
public class ScrollCommand : ICommand
{

    private Inventory inventory;
    private int count;

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
