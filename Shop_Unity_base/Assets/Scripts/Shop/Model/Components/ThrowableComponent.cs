namespace Model
{
    using Model.Items;

    public class ThrowableComponent : Component<Item>
    {
        public float Range { get; private set; }

        public ThrowableComponent(float range)
        {
            Range = range;
        }

        public ThrowableComponent(ThrowableComponent original)
        {
            CloneMembers(original);
        }

        public void CloneMembers(ThrowableComponent original)
        {
            Range = original.Range;
        }

        public void Throw()
        {
            // Pseudo code for measuring distance, always succeeds now
            float distanceToTarget = 0;

            if (distanceToTarget <= Range)
            {
                // TODO do something with this
                TradeLog.AddMessage($"WOW YOU HIT A THROWING {Owner.Name}");
            }

            // Miss
        }

        public override IClonable Clone()
        {
            return new ThrowableComponent(this);
        }
    }
}
