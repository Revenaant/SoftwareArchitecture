namespace Model.Items
{
    public class MiscItem : Item
    {
        public MiscItem(string name, int cost, int quantity = 1) : base(name, cost, quantity)
        {
            AddComponent(new DrawableComponent(iconName: "item", name, cost, quantity));
        }

        private MiscItem(MiscItem original)
        {
            CloneMembers(original);
        }

        public override IClonable Clone()
        {
            return new MiscItem(this);
        }
    }
}
