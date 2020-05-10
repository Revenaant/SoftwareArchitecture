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

        private DrawableComponent(DrawableComponent drawableComponent)
        {
            CloneMembers(drawableComponent);
        }

        private void CloneMembers(DrawableComponent drawableComponent)
        {
            DisplayName = drawableComponent.DisplayName;
            DisplayCost = drawableComponent.DisplayCost;
            IconName = drawableComponent.IconName;
        }
    }
}
