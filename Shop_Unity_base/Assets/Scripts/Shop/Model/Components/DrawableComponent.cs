namespace Model
{
    public class DrawableComponent : Component<Item>
    {
        public string DisplayName { get; private set; }
        public int DisplayCost { get; private set; }
        public string IconName { get; private set; }

        public DrawableComponent(string displayName, int displayCost, string iconName)
        {
            DisplayName = displayName;
            DisplayCost = displayCost;
            IconName = iconName;
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
            DisplayName = original.DisplayName;
            DisplayCost = original.DisplayCost;
            IconName = original.IconName;
        }
    }
}
