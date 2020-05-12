namespace Model
{
    using System.Collections.Generic;
    using Utility;

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

        public bool Contains(Item item)
        {
            return items.Contains(item);
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

        public Item GetItemByName(string name)
        {
            for (int i = 0; i < ItemCount; i++)
            {
                if (items[i].Name.Equals(name))
                    return items[i];
            }

            return null;
        }

        public int IndexOfItem(Item item)
        {
            int index = items.IndexOf(item);

            if (index < 0)
                throw new System.IndexOutOfRangeException($"The item collection does not contain an index for item {item}");

            return index;
        }

        public Item GetSelectedItem()
        {
            return GetItemByIndex(selectedItemIndex);
        }

        public int GetSelectedItemIndex()
        {
            return selectedItemIndex;
        }

        public void SelectItem(Item item)
        {
            if (item != null)
            {
                int index = IndexOfItem(item);
                if (index >= 0)
                    selectedItemIndex = index;
            }
        }

        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < ItemCount)
                selectedItemIndex = index;
        }

        public List<Item> GetItems()
        {
            return new List<Item>(Items);
        }
    }
}
