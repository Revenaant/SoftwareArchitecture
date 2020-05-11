namespace Model.Items
{
    public class Potion : Item
    {
        public Potion(string name, int cost, int quantity = 1) : base(name, cost, quantity)
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
