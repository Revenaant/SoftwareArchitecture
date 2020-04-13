namespace Model
{
    using System;
    using System.Collections.Generic;

    //This class holds the model of our Shop. It contains an ItemList. In its current setup, view and controller need to get
    //data via polling. Advisable is, to set up an event system for better integration with View and Controller.
    public class ShopModel
    {
        const int MaxMessageQueueCount = 4; //it caches the last four messages
        private List<string> messages = new List<string>();

        private List<Item> itemList = new List<Item>(); //items in the store
        private int selectedItemIndex = 0; //selected item index

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopModel()
        //------------------------------------------------------------------------------------------------------------------------        
        public ShopModel()
        {
            PopulateInventory(16); //currently, it has 16 items
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  PopulateInventory()
        //------------------------------------------------------------------------------------------------------------------------        
        private void PopulateInventory(int itemCount)
        {
            for (int index = 0; index < itemCount; index++)
            {
                Item item = new Item("item", "item", 10); //item name, item icon, cost
                itemList.Add(item);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetSelectedItem()
        //------------------------------------------------------------------------------------------------------------------------        
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

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItem()
        //------------------------------------------------------------------------------------------------------------------------
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

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItemByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        //attempts to select the item, specified by 'index', fails silently
        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
            {
                selectedItemIndex = index;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetSelectedItemIndex()
        //------------------------------------------------------------------------------------------------------------------------
        //returns the index of the current selected item
        public int GetSelectedItemIndex()
        {
            return selectedItemIndex;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns a list with all current items in the shop.
        public List<Item> GetItems()
        {
            return new List<Item>(itemList); //returns a copy of the list, so the original is kept intact, 
                                             //however this is shallow copy of the original list, so changes in 
                                             //the original list will likely influence the copy, apply 
                                             //creational patterns like prototype to fix this. 
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItemCount()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the number of items
        public int GetItemCount()
        {
            return itemList.Count;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItemByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
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

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetMessage()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the cached list of messages
        public string[] GetMessages()
        {
            return messages.ToArray();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  AddMessage()
        //------------------------------------------------------------------------------------------------------------------------
        //adds a message to the cache, cleaning it up if the limit is exceeded
        private void AddMessage(string message)
        {
            messages.Add(message);
            while (messages.Count > MaxMessageQueueCount)
            {
                messages.RemoveAt(0);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Buy()
        //------------------------------------------------------------------------------------------------------------------------        
        //not implemented yet
        public void Buy()
        {
            AddMessage("You can't buy this item yet!");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Sell()
        //------------------------------------------------------------------------------------------------------------------------        
        //not implemented yet
        public void Sell()
        {
            AddMessage("You can't sell this item yet!");
        }

    }
}
