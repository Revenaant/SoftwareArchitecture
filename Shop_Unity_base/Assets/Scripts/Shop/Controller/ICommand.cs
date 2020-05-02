namespace Controller
{
    public interface ICommand
    {
        void Execute(InventoryController controller);
    }

    public class NullCommand : ICommand
    {
        void ICommand.Execute(InventoryController controller)
        {
            // Do nothing
        }
    }

    public class ClearCommand : ICommand
    {
        void ICommand.Execute(InventoryController controller)
        {
            controller.ClearInventory();
        }
    }

    public class SortCommand : ICommand
    {
        void ICommand.Execute(InventoryController controller)
        {
            controller.SortInventory();
        }
    }

    public class RestockCommand : ICommand
    {
        void ICommand.Execute(InventoryController controller)
        {
            controller.RestockInventory();
        }
    }

    public class SellCommand : ICommand
    {
        void ICommand.Execute(InventoryController controller)
        {
            controller.Sell();
        }
    }
}
