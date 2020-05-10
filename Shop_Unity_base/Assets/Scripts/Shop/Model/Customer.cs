namespace Model
{
    public class CustomerModel : TraderModel
    {
        private const int INVENTORY_CAPACITY = 24;

        public CustomerModel(string name, int startingGold) : base()
        {
            Initialize(name, startingGold, INVENTORY_CAPACITY);
        }

        protected override void Initialize(string name, int startingGold, int inventoryCapacity)
        {
            Name = name;
            Gold = startingGold;
            Inventory = new Inventory(inventoryCapacity);
        }

        public override void Restock()
        {
            AddGold(GetGoldFromAdventure());
        }

        // Simulate adventuring functionality
        private int GetGoldFromAdventure()
        {
            return Utility.Random.Get(200, 350);
        }
    }
}
