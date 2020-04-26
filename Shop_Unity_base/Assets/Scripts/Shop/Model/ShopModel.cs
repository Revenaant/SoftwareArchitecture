namespace Model
{
    using System;
    using System.Collections.Generic;

    //This class holds the model of our Shop. It contains an ItemList. In its current setup, view and controller need to get
    //data via polling. Advisable is, to set up an event system for better integration with View and Controller.

    public class ShopModel
    {
        private int gold;
        private const int STARTING_GOLD = 1000;

        const int MaxMessageQueueCount = 4; //it caches the last four messages
        private List<string> messages = new List<string>();

        private List<Item> itemList = new List<Item>(); //items in the store
        private int selectedItemIndex = 0; //selected item index
        private Customer customer;
  
        public ShopModel()
        {
            PopulateInventory(16); //currently, it has 16 items
            gold = STARTING_GOLD;
        }
    
        private void PopulateInventory(int itemCount)
        {
            for (int index = 0; index < itemCount; index++)
            {
                Item item = new Item("item", "item", 10); //item name, item icon, cost
                itemList.Add(item);
            }
        }
   
        //returns the selected item
        public Item GetSelectedItem()
        {
            if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Count)
            {
                return itemList[selectedItemIndex];
            }
            else
            {
                return null;
            }
        }

        //attempts to select the given item, fails silently
        public void SelectItem(Item item)
        {
            if (item != null)
            {
                int index = itemList.IndexOf(item);
                if (index >= 0)
                {
                    selectedItemIndex = index;
                }
            }
        }
 
        //attempts to select the item, specified by 'index', fails silently
        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
            {
                selectedItemIndex = index;
            }
        }

        //returns the index of the current selected item
        public int GetSelectedItemIndex()
        {
            return selectedItemIndex;
        }
    
        //returns a list with all current items in the shop.
        public List<Item> GetItems()
        {
            return new List<Item>(itemList); //returns a copy of the list, so the original is kept intact, 
                                             //however this is shallow copy of the original list, so changes in 
                                             //the original list will likely influence the copy, apply 
                                             //creational patterns like prototype to fix this. 
        }
      
        //returns the number of items
        public int GetItemCount()
        {
            return itemList.Count;
        }

        //tries to get an item, specified by index. returns null if unsuccessful
        public Item GetItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
            {
                return itemList[index];
            }
            else
            {
                return null;
            }
        }
    
        //returns the cached list of messages
        public string[] GetMessages()
        {
            return messages.ToArray();
        }

        //adds a message to the cache, cleaning it up if the limit is exceeded
        private void AddMessage(string message)
        {
            messages.Add(message);
            while (messages.Count > MaxMessageQueueCount)
            {
                messages.RemoveAt(0);
            }
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
        }
    
        public Item Buy()
        {
            Item item = GetSelectedItem();

            if(customer.Gold < item.Cost)
            {
                AddMessage("You do not have enough gold to buy that item!");
                AddMessage("Item cost: " + item.Cost + ", Your gold: " + customer.Gold);
                return null;
            }

            // Transaction
            customer.Purchase(item);
            gold += item.Cost;

            itemList.Remove(item);
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

            customer.Sell(item);
            itemList.Add(item);
        }

    }
}
