namespace Controller
{
    using System;
    using System.Collections.Generic;
    using Model;
    using Model.Items;

    // This class provides a controller for an InventoryController. The Controller acts as a public interface for a ShopModel.
    // These methods are being called by ShopView, as it implements the user interface. The exception is Initialize(),
    // it is being called by ShopState. We use Initialize() as a replacement for the constructor, as this class is a MonoBehaviour.
    public class InventoryController
    {
        private ITrader trader;
        public ITrader Trader => trader;

        private ITrader otherTrader;

        // Ties this controller to a model
        public InventoryController(ITrader trader, ITrader otherTrader)
        {
            this.trader = trader;
            this.otherTrader = otherTrader;
            Browse();
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

        public void ClearInventory()
        {
            trader.Inventory.ClearInventory();
        }

        public void SortInventory()
        {
            trader.Inventory.SortInventory();
        }

        public void RestockInventory()
        {
            trader.Restock();
        }
    }
}
