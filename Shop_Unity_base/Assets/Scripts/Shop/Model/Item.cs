namespace Model
{
    //This class holds data for an Item. Currently it has a name, an iconName and an amount.
    public class Item
    {
        public readonly string name;
        public readonly string iconName;
        public int amount { get; private set; }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Item()
        //------------------------------------------------------------------------------------------------------------------------
        public Item(string name, string iconName, int amount)
        {
            this.name = name;
            this.iconName = iconName;
            this.amount = amount;
        }

    }
}
