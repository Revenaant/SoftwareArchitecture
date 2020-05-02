namespace Model
{
    using Model.Items;
    using System;
    using Utility;

    public class Customer : ITrader
    {
        private ITrader otherTrader;

        private Inventory inventory;
        public Inventory Inventory => inventory;

        public string Name { get; private set; }
        public int Gold { get; private set; }

        private Events.SellDelegate onItemSold;
        public event Events.SellDelegate OnItemSoldEvent
        {
            add { onItemSold += value; }
            remove { onItemSold -= value; }
        }

        public Customer(string name)
        {
            Name = name;
            Initialize();
        }

        public void Initialize()
        {
            inventory = new Inventory(24);
            Gold = 500;
        }

        ~Customer()
        {
            otherTrader.OnItemSoldEvent -= ((ITrader)this).OnItemBought;
            onItemSold = null;
        }

        public void SetOtherTrader(ITrader trader)
        {
            if (otherTrader != null)
                otherTrader.OnItemSoldEvent -= ((ITrader)this).OnItemBought;

            otherTrader = trader;

            otherTrader.OnItemSoldEvent += ((ITrader)this).OnItemBought;
        }

        private void AddGold(int value)
        {
            Gold += value;
            Gold.Clamp(0, 99999);
        }

        void ITrader.Sell(ITrader buyer)
        {
            ITradeable tradeable = inventory.GetSelectedItem();

            if (buyer.Gold <= 0 || buyer.Gold < tradeable.Cost)
                return;

            AddGold(tradeable.Cost);
            Inventory.Remove((Item)tradeable);
            onItemSold?.Invoke(tradeable, buyer);
        }

        void ITrader.OnItemBought(ITradeable tradeable, ITrader seller)
        {
            AddGold(-tradeable.Cost);
            Inventory.Add((Item)tradeable);
        }

        void ITrader.Restock()
        {
            // Uhm.. Go on an adventure?
        }
    }
}
