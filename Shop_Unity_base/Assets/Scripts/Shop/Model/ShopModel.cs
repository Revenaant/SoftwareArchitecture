namespace Model
{
    using Model.Items;
    using System;
    using System.Collections.Generic;
    using Utility;

    // This class holds the model of our Shop. It contains an ItemList. In its current setup, view and controller need to get
    // data via polling. Advisable is, to set up an event system for better integration with View and Controller.
    public class ShopModel : ITrader
    {
        private const int STARTING_GOLD = 1000;

        private ITrader otherTrader;
        private const int MAX_MESSAGES_IN_QUEUE = 4;
        private Queue<string> messages = new Queue<string>();

        public string Name => "Shop";

        private int gold;
        public int Gold => gold;

        private Inventory inventory;
        public Inventory Inventory => inventory;

        private Events.SellDelegate onItemSold;
        public event Events.SellDelegate OnItemSoldEvent
        {
            add { onItemSold += value; }
            remove { onItemSold -= value; }
        }

        public ShopModel()
        {
            PopulateInventory(24);
            gold = STARTING_GOLD;
        }

        ~ShopModel()
        {
            otherTrader.OnItemSoldEvent -= ((ITrader)this).OnItemBought;
            onItemSold = null;
        }

        private void PopulateInventory(int itemCount)
        {
            inventory = new Inventory(itemCount);

            // TODO: could change to IFactory factory = new ItemFactory<WeaponFactory>(); ?
            WeaponFactory weaponFactory = new WeaponFactory();
            PotionFactory potionFactory = new PotionFactory();

            for (int index = 0; index < itemCount; index++)
            {
                Item item = Utility.Random.Instance.Next(0, 2) == 0
                    ? (Item)weaponFactory.CreateRandomWeapon()
                    : (Item)potionFactory.CreateRandomPotion();

                inventory.Add(item);
            }
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

        public void SetOtherTrader(ITrader trader)
        {
            if (otherTrader != null)
                otherTrader.OnItemSoldEvent -= ((ITrader)this).OnItemBought;

            otherTrader = trader;

            otherTrader.OnItemSoldEvent += ((ITrader)this).OnItemBought;
        }

        // TODO sent messages have old data
        private void OnCustomerBuyItem(Item item, ITrader other)
        {
            AddGold(item.Cost);
            inventory.Remove(item);

            AddMessage($"{otherTrader.Name} purchased [{item.Name}] from the shop for -{item.Cost} gold");
            AddMessage($"{otherTrader.Name}'s gold: {otherTrader.Gold}");
        }

        private void OnCustomerSellItem(Item item, ITrader other)
        {
            AddGold(-item.Cost);
            inventory.Add(item);

            AddMessage($"{otherTrader.Name} sold [{item.Name}] to the shop for +{item.Cost} gold");
            AddMessage($"{otherTrader.Name}'s gold: {otherTrader.Gold}");
        }

        private void AddGold(int value)
        {
            gold += value;
            gold.Clamp(0, 99999);
        }

        void ITrader.Sell(ITrader buyer)
        {
            ITradeable tradeable = inventory.GetSelectedItem();

            if (buyer.Gold <= 0 || buyer.Gold < tradeable.Cost)
            {
                // TODO make this static in MessageView
                AddMessage($"Trader {buyer.GetType().Name} does not have enough gold to buy that!");
                AddMessage($"{buyer.GetType().Name}'s gold: {buyer.Gold}, item cost: {tradeable.Cost}");
                return;
            }

            // TODO add Clone so it can be purchased multiple times
            AddGold(tradeable.Cost);
            onItemSold?.Invoke(tradeable, buyer);
            Inventory.Remove((Item)tradeable);

            AddMessage($"Trader {buyer.GetType().Name} just bought [{tradeable.Name}] for {tradeable.Cost} Gold from {this.GetType().Name}!");
            AddMessage($"{buyer.GetType().Name}'s remaining gold: {buyer.Gold}");

            // if(--Stock<tradable> <= 0)
            //  Inventory.Remove(tradeable);
        }

        void ITrader.OnItemBought(ITradeable tradeable, ITrader seller)
        {
            AddGold(-tradeable.Cost);
            Inventory.Add((Item)tradeable);

            AddMessage($"Trader {this.GetType().Name} just bought [{tradeable.Name}] for {tradeable.Cost} Gold from {seller.GetType().Name}!");
            AddMessage($"{seller.GetType().Name}'s remaining gold: {seller.Gold}");
        }

        // TODO
        void ITrader.Restock()
        {
            throw new NotImplementedException();
        }
    }
}
