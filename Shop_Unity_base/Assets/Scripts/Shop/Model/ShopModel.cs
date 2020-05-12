namespace Model
{
    using Model.Items;
    using System.Collections.Generic;
    using Utility;

    public class ShopModel : TraderModel
    {
        private const int INVENTORY_CAPACITY = 25;
        private const int STARTING_GOLD = 1000;
        private const int MIN_ITEMS = 10;
        private const int MAX_ITEMS = 18;
        private const int MAX_QUANTITY = 5;

        private List<IItemFactory> itemFactories = new List<IItemFactory>();

        public ShopModel(string name = "Shop", int startingGold = STARTING_GOLD) : base()
        {
            Initialize(name, startingGold, INVENTORY_CAPACITY);
        }

        protected override void Initialize(string name, int startingGold, int inventoryCapacity)
        {
            Name = name;
            Gold = startingGold;
            Inventory = new Inventory(inventoryCapacity);

            InitializeFactories();
            PopulateInventory(Random.Get(MIN_ITEMS, MAX_ITEMS));
        }

        private void InitializeFactories()
        {
            itemFactories.Add(new WeaponFactory());
            itemFactories.Add(new PotionFactory());
            itemFactories.Add(new MiscItemFactory());
        }

        private void PopulateInventory(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                int index = Random.Get(0, itemFactories.Count);
                Item item = itemFactories[index].CreateRandom();
                item.Quantity = (Random.Get(1, MAX_QUANTITY));

                Inventory.Add(item);
            }
        }

        public override void Restock()
        {
            Inventory.ClearInventory();
            PopulateInventory(Random.Get(MIN_ITEMS, MAX_ITEMS));
            NotifyRedrawObservers(new RedrawNotification($"{Name} restocked, new items are available"));
        }
    }
}
