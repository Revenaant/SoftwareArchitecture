using Model.Items;

namespace Model
{
    // This class holds data for an Item. Currently it has a name, an iconName and an amount.
    public abstract class Item : IClonable, ITradeable
    {
        public enum ItemType { Potion, Weapon }

        protected string name;
        protected string iconName;
        protected int cost;

        protected Item()
        {
            iconName = "item";
        }

        protected Item(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
            iconName = "item";
        }

        protected virtual void CloneFields(Item item)
        {
            name = item.name;
            iconName = item.iconName;
            cost = item.cost;
        }

        public string Name => name;
        public string IconName => iconName;
        public int Cost => cost;

        public abstract IClonable Clone();
    }
}
