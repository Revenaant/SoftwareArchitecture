namespace Model
{
    using Model.Items;
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

        private Inventory inventory;
        public Inventory Inventory => inventory;

        public ShopModel()
        {
            PopulateInventory(24);
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

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public Item Buy()
        {
            Item item = inventory.GetSelectedItem();

            if (customer.Gold < item.Cost)
            {
                AddMessage("You do not have enough gold to buy that item!");
                AddMessage($"Item cost: {item.Cost}, Your gold: {customer.Gold}");
                return null;
            }

            OnBuyEvent?.Invoke(item, customer);
            return item;
        }

        // We assume that the customer has the same type of items that the shop has
        public void Sell()
        {
            Item item = inventory.GetSelectedItem();

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
            inventory.Remove(item);

            AddMessage($"{customer.Name} purchased [{item.Name}] from the shop for -{item.Cost} gold");
            AddMessage($"{customer.Name}'s gold: {customer.Gold}");
        }

        private void OnCustomerSellItem(Item item, Customer customer)
        {
            AddGold(-item.Cost);
            inventory.Add(item);

            AddMessage($"{customer.Name} sold [{item.Name}] to the shop for +{item.Cost} gold");
            AddMessage($"{customer.Name}'s gold: {customer.Gold}");
        }

        private void AddGold(int value)
        {
            gold += value;
            gold.Clamp(0, 99999);
        }
    }
}
