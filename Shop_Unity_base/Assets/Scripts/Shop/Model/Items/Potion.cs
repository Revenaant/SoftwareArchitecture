namespace Model.Items
{
    public class Potion : Item, IClonable
    {
        private int useAmount;
        public int UseAmount => useAmount;

        public Potion(string name, int cost, int useAmount) : base(name, cost)
        {
            this.useAmount = useAmount;
            iconName = "itemPotion";
        }

        public Potion(Potion potion)
        {
            CloneFields(potion);
        }

        protected override void CloneFields(Item item)
        {
            base.CloneFields(item);

            Potion referencePotion = (Potion)item;
            useAmount = referencePotion.useAmount;
        }

        public override IClonable Clone()
        {
            return new Potion(this);
        }
    }
}
