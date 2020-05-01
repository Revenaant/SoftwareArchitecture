namespace Model
{
    using System;
    using System.Collections.Generic;
    using Utility;

    public class Customer
    {
        private Inventory inventory;
        public Inventory Inventory => inventory;

        public string Name { get; private set; }
        public int Gold { get; private set; }

        public Customer(string name)
        {
            Name = name;
            Initialize();
        }

        public void Initialize()
        {
            inventory = new Inventory(24);
            Gold = 500;

            ShopModel.OnBuyEvent += OnBuyItem;
            ShopModel.OnSellEvent += OnSellItem;
        }

        ~Customer()
        {
            ShopModel.OnBuyEvent -= OnBuyItem;
            ShopModel.OnSellEvent -= OnSellItem;
        }

        public void OnBuyItem(Item item, Customer customer)
        {
            AddGold(-item.Cost);
            inventory.Add(item);
        }

        public void OnSellItem(Item item, Customer customer)
        {
            AddGold(item.Cost);
            inventory.Remove(item);
        }

        private void AddGold(int value)
        {
            Gold += value;
            Gold.Clamp(0, 99999);
        }
    }
}
