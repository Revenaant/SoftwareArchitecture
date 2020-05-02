namespace Controller
{
    using Model;

    public interface ICommand
    {
        void Execute(Inventory inventory);
    }

    public class ClearCommand : ICommand
    {
        void ICommand.Execute(Inventory inventory)
        {
            //inventory.Clear;
            throw new System.NotImplementedException();
        }
    }

    public class SortCommand : ICommand
    {
        void ICommand.Execute(Inventory inventory)
        {
            //inventory.Sort;
            throw new System.NotImplementedException();
        }
    }

    public class RestockCommand : ICommand
    {
        void ICommand.Execute(Inventory inventory)
        {
            //inventory.Restock;
            throw new System.NotImplementedException();
        }
    }

    public class BuyCommand : ICommand
    {
        void ICommand.Execute(Inventory inventory)
        {
            //inventory.Buy;
            throw new System.NotImplementedException();
        }
    }

    public class SellCommand : ICommand
    {
        void ICommand.Execute(Inventory inventory)
        {
            //inventory.Sell;
            throw new System.NotImplementedException();
        }
    }
}
