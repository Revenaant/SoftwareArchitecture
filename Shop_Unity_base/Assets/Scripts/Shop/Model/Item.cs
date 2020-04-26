namespace Model
{
    // This class holds data for an Item. Currently it has a name, an iconName and an amount.
    public class Item
    {
        public readonly string name;
        public readonly string iconName;
        public int Cost { get; private set; }

        public Item(string name, string iconName, int cost)
        {
            this.name = name;
            this.iconName = iconName;
            this.Cost = cost;
        }
    }
}
