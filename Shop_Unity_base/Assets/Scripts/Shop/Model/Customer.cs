using System;
using System.Collections.Generic;

namespace Model
{
    public class Customer
    {
        private Inventory inventory;
        public int Gold { get; private set; }

        public Customer()
        {
            Initialize();
        }

        public void Initialize()
        {
            inventory = new Inventory(16);
            Gold = 500;
        }

        public void Purchase(Item item)
        {
            Gold = Math.Max(Gold - item.Cost, 0);
            inventory.AddItem(item);
        }

        public void Sell(Item item)
        {
            Gold += item.Cost;
            inventory.RemoveItem(item);
        }
    }

    public class Inventory
    {
        public readonly List<Item> items;

        public Inventory(int size)
        {
            items = new List<Item>(size);
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if (items.Contains(item))
                items.Remove(item);
        }
    }
}
