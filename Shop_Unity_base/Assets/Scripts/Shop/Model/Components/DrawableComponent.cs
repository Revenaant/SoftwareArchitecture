namespace Model
{
    public class DrawableComponent : CustomComponent<Item>
    {
        public string IconName { get; private set; }
        public string DisplayName { get; private set; }
        public int DisplayCost { get; private set; }

        private int displayQuantity;
        public int DisplayQuantity => displayQuantity;

        public DrawableComponent(string iconName, string displayName, int displayCost, int displayQuantity)
        {
            IconName = iconName;
            DisplayName = displayName;
            DisplayCost = displayCost;
            this.displayQuantity = displayQuantity;
        }

        public void SetQuantity(int value)
        {
            displayQuantity = value;
        }

        public override IClonable Clone()
        {
            return new DrawableComponent(this);
        }

        private DrawableComponent(DrawableComponent original)
        {
            CloneMembers(original);
        }

        private void CloneMembers(DrawableComponent original)
        {
            IconName = original.IconName;
            DisplayName = original.DisplayName;
            DisplayCost = original.DisplayCost;
            displayQuantity = original.DisplayQuantity;
        }
    }
}
