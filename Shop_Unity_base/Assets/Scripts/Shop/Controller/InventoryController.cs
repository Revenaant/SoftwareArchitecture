namespace Controller
{
    using System;
    using System.Collections.Generic;
    using Model;
    using Model.Items;

    // This class provides a controller for an InventoryController. The Controller acts as a public interface for a ShopModel.
    // These methods are being called by ShopView, as it implements the user interface. The exception is Initialize(),
    // it is being called by ShopState. We use Initialize() as a replacement for the constructor, as this class is a MonoBehaviour.
    public class InventoryController : IObservable<RedrawNotification>
    {
        private List<IObserver<RedrawNotification>> observers;

        private ITrader trader;
        public ITrader Trader => trader;

        private ITrader otherTrader;

        // Ties this controller to a model
        public InventoryController(ITrader trader, ITrader otherTrader)
        {
            observers = new List<IObserver<RedrawNotification>>();

            this.trader = trader;
            this.otherTrader = otherTrader;
            Browse();
        }

        ~InventoryController()
        {
            for (int i = 0; i < observers.Count; i++)
                observers[i].OnCompleted();
        }

        public List<Item> GetItems()
        {
            return trader.Inventory.GetItems();
        }

        public int GetItemCount()
        {
            return trader.Inventory.ItemCount;
        }

        public Item GetItemByIndex(int index)
        {
            return trader.Inventory.GetItemByIndex(index);
        }

        public Item GetSelectedItem()
        {
            return trader.Inventory.GetSelectedItem();
        }

        public int GetSelectedItemIndex()
        {
            return trader.Inventory.GetSelectedItemIndex();
        }

        // Attempt to select an item
        public void SelectItem(Item item)
        {
            if (item != null)
                trader.Inventory.SelectItem(item);
        }

        public void SelectItemByIndex(int index)
        {
            Item item = trader.Inventory.GetItemByIndex(index);
            if (item != null)
                trader.Inventory.SelectItem(item);
        }

        public void Browse()
        {
            // Right now all this function does is select the first item in shopModel.
            trader.Inventory.SelectItemByIndex(0);
        }

        public void Sell()
        {
            trader.Sell(otherTrader);
        }

        public void Consume()
        {
            IConsumer customer;
            if (trader is IConsumer)
            {
                customer = trader as IConsumer;
                customer.Consume(GetSelectedItem());
                return;
            }

            NotifyObservers(new RedrawNotification($"{trader.Name} cannot consume that item because it is not consumable!"));
        }

        public void ClearInventory()
        {
            trader.Inventory.ClearInventory();
            NotifyObservers(new RedrawNotification($"{trader.Name}'s Inventory has been cleared"));
        }

        public void SortInventory()
        {
            trader.Inventory.SortInventory();
            NotifyObservers(new RedrawNotification($"{trader.Name}'s inventory is now sorted by name"));
        }

        public void RestockInventory()
        {
            trader.Restock();
        }

        public IDisposable Subscribe(IObserver<RedrawNotification> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber<RedrawNotification>(observers, observer);
        }

        protected void NotifyObservers(RedrawNotification notification)
        {
            for (int i = 0; i < observers.Count; i++)
                observers[i].OnNext(notification);
        }
    }
}
