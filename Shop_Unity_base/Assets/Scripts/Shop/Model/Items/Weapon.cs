using Utility;

namespace Model.Items
{
    public class Weapon : Item, IClonable
    {
        public Weapon(string name, int cost, int quantity = 1) : base(name, cost, quantity)
        {
            if (!TypeToComponent.ContainsKey(typeof(DrawableComponent)))
                AddComponent(new DrawableComponent(iconName: "itemWeapon", name, cost, quantity));
        }

        private Weapon(Weapon original)
        {
            CloneMembers(original);
        }

        public override IClonable Clone()
        {
            return new Weapon(this);
        }
    }
}
