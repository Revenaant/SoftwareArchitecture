namespace Model
{
    using System;
    using System.Collections.Generic;
    using Utility;

    // This class holds the model of our Shop. It contains an ItemList. In its current setup, view and controller need to get
    // data via polling. Advisable is, to set up an event system for better integration with View and Controller.
    public class ShopModel
    {
        public static event Events.BuyDelegate OnBuyEvent;
        public static event Events.SellDelegate OnSellEvent;

        private const int STARTING_GOLD = 1000;
        private Customer customer;
        private int gold;

        private const int MAX_MESSAGES_IN_QUEUE = 4;
        private Queue<string> messages = new Queue<string>();

        private List<Item> itemList = new List<Item>();
        private int selectedItemIndex = 0;

        public ShopModel()
        {
            PopulateInventory(16);
            gold = STARTING_GOLD;

            OnBuyEvent += OnCustomerBuyItem;
            OnSellEvent += OnCustomerSellItem;
        }

        ~ShopModel()
        {
            OnBuyEvent -= OnCustomerBuyItem;
            OnSellEvent -= OnCustomerSellItem;
        }

        private void PopulateInventory(int itemCount)
        {
            for (int index = 0; index < itemCount; index++)
            {
                Item item = new Item(name: "item", iconName: "item", cost: 10);
                itemList.Add(item);
            }
        }

        // Returns the selected item
        public Item GetSelectedItem()
        {
            if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Count)
                return itemList[selectedItemIndex];
            else
                return null;
        }

        // Attempts to select the given item, fails silently
        public void SelectItem(Item item)
        {
            if (item != null)
            {
                int index = itemList.IndexOf(item);
                if (index >= 0)
                    selectedItemIndex = index;
            }
        }

        // Attempts to select the item, specified by 'index', fails silently
        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
                selectedItemIndex = index;
        }

        // Returns the index of the current selected item
        public int GetSelectedItemIndex()
        {
            return selectedItemIndex;
        }

        // Returns a list with all current items in the shop.
        public List<Item> GetItems()
        {
            return new List<Item>(itemList); // returns a copy of the list, so the original is kept intact, 
                                             // however this is shallow copy of the original list, so changes in 
                                             // the original list will likely influence the copy, apply 
                                             // creational patterns like prototype to fix this. 
        }

        // Returns the number of items
        public int GetItemCount()
        {
            return itemList.Count;
        }

        // Rries to get an item, specified by index. returns null if unsuccessful
        public Item GetItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
                return itemList[index];
            else
                return null;
        }

        // Returns the cached list of messages
        public string[] GetMessages()
        {
            // TODO this defeats the purpose of a queue
            string[] queuedMessages = messages.ToArray();
            messages.Clear();

            return queuedMessages;
        }

        // Adds a message to the cache, cleaning it up if the limit is exceeded
        private void AddMessage(string message)
        {
            messages.Enqueue(message);

            while (messages.Count > MAX_MESSAGES_IN_QUEUE)
                messages.Dequeue();
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public Item Buy()
        {
            Item item = GetSelectedItem();

            if (customer.Gold < item.Cost)
            {
                AddMessage("You do not have enough gold to buy that item!");
                AddMessage("Item cost: " + item.Cost + ", Your gold: " + customer.Gold);
                return null;
            }

            OnBuyEvent?.Invoke(item, customer);
            return item;
        }

        // We assume that the customer has the same type of items that the shop has
        public void Sell()
        {
            Item item = GetSelectedItem();

            if (gold >= 0 && gold < item.Cost)
            {
                AddMessage("The shop does not have enough gold to buy that item!");
                return;
            }

            OnSellEvent?.Invoke(item, customer);
        }

        // TODO sent messages have old data
        private void OnCustomerBuyItem(Item item, Customer customer)
        {
            AddGold(item.Cost);
            itemList.Remove(item);

            AddMessage($"{customer.Name} purchased [{item.name}] from the shop for -{item.Cost} gold");
            AddMessage($"{customer.Name}'s gold: {customer.Gold}");
        }

        private void OnCustomerSellItem(Item item, Customer customer)
        {
            AddGold(-item.Cost);
            itemList.Add(item);

            AddMessage($"{customer.Name} sold [{item.name}] to the shop for +{item.Cost} gold");
            AddMessage($"{customer.Name}'s gold: {customer.Gold}");
        }

        private void AddGold(int value)
        {
            gold += value;
            gold.Clamp(0, 99999);
        }
    }
}
