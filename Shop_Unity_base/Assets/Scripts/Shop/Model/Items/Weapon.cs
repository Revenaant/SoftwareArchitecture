using Utility;

namespace Model.Items
{
    public class Weapon : Item, IClonable
    {
        public Weapon(string name, int cost) : base(name, cost)
        {
            if (!TypeToComponent.ContainsKey(typeof(DrawableComponent)))
                AddComponent(new DrawableComponent(name, cost, iconName: "itemWeapon"));
        }

        private Weapon(Weapon weapon)
        {
            CloneMembers(weapon);
        }

        public override IClonable Clone()
        {
            return new Weapon(this);
        }
    }
}
