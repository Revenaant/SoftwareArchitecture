namespace Model.Items
{
    public class Potion : Item
    {
        public Potion(string name, int cost) : base(name, cost)
        {
            AddComponent(new DrawableComponent(name, cost, iconName: "itemPotion"));
        }

        private Potion(Potion original)
        {
            CloneMembers(original);
        }

        public override IClonable Clone()
        {
            return new Potion(this);
        }
    }
}
