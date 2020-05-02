namespace Model
{
    using System.Collections.Generic;
    using Utility;

    // TODO Generics
    public class Inventory
    {
        private int selectedItemIndex = 0;

        private List<Item> items;
        public List<Item> Items => items;

        public int Capacity { get; private set; }
        public int ItemCount => items.Count;

        public Inventory(int size)
        {
            Capacity = size;
            items = new List<Item>(Capacity);
        }

        public void Add(Item item)
        {
            if (ItemCount < Capacity)
                items.Add(item);
        }

        public void Remove(Item item)
        {
            if (items.Contains(item))
                items.Remove(item);
        }

        public void ClearInventory()
        {
            items.Clear();
        }

        public void SortInventory()
        {
            items.Sort();
        }

        public Item GetItem(Item item)
        {
            if (!items.Contains(item))
                return null;

            return items.Find(i => i == item);
        }

        public Item GetItemByIndex(int index)
        {
            if (ItemCount <= 0)
                return null;

            // Enforce valid indexes
            return items[index.Clamp(0, ItemCount - 1)];
        }

        public int IndexOfItem(Item item)
        {
            int index = items.IndexOf(item);

            if (index < 0)
                throw new System.IndexOutOfRangeException($"The item collection does not contain an index for item {item}");

            return index;
        }

        // TODO should these methods go in a separate "controller" type class?
        // Returns the selected item
        public Item GetSelectedItem()
        {
            return GetItemByIndex(selectedItemIndex);
        }

        // Returns the index of the current selected item
        public int GetSelectedItemIndex()
        {
            return selectedItemIndex;
        }

        // Attempts to select the given item, fails silently
        public void SelectItem(Item item)
        {
            if (item != null)
            {
                int index = IndexOfItem(item);
                if (index >= 0)
                    selectedItemIndex = index;
            }
        }

        // Attempts to select the item, specified by 'index', fails silently
        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < ItemCount)
                selectedItemIndex = index;
        }


        /// <summary>
        /// Returns a shallow copy of the list with all current items in the shop.
        /// </summary>
        public List<Item> GetItems()
        {
            return new List<Item>(Items); // TODO, this returns a copy of the list, so the original is kept intact, 
                                          // however this is shallow copy of the original list, so changes in 
                                          // the original list will likely influence the copy, apply 
                                          // creational patterns like prototype to fix this. 
        }
    }
}
