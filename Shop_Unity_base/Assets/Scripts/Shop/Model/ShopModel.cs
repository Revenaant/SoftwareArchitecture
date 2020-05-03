namespace Model
{
    using Model.Items;

    public class ShopModel : TraderModel
    {
        private const int INVENTORY_CAPACITY = 24;
        private const int STARTING_GOLD = 1000;

        public ShopModel(string name = "Shop", int startingGold = STARTING_GOLD) : base()
        {
            Initialize(name, startingGold, INVENTORY_CAPACITY);
        }

        protected override void Initialize(string name, int startingGold, int inventoryCapacity)
        {
            Name = name;
            Gold = startingGold;
            Inventory = new Inventory(inventoryCapacity);
            PopulateInventory(Utility.Random.Get(16, 24));
        }

        private void PopulateInventory(int itemCount)
        {
            WeaponFactory weaponFactory = new WeaponFactory();
            PotionFactory potionFactory = new PotionFactory();

            for (int index = 0; index < itemCount; index++)
            {
                Item item = Utility.Random.Get(0, 2) == 0
                    ? (Item)weaponFactory.CreateRandomWeapon()
                    : (Item)potionFactory.CreateRandomPotion();

                Inventory.Add(item);
            }
        }

        public override void Restock()
        {
            Inventory.ClearInventory();
            PopulateInventory(Utility.Random.Get(16, 24));
        }
    }
}
