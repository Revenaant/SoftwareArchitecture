namespace Model
{
    public class ThrowableComponent : Component<Item>
    {
        public float Range { get; private set; }

        public ThrowableComponent(float range)
        {
            Range = range;
        }

        public void Throw()
        {
            // Pseudo code for measuring distance, always succeeds now
            float distanceToTarget = 0;

            if (distanceToTarget <= Range)
                TradeLog.AddMessage($"{Owner.Name} was thrown and hit successfully");
            else
                TradeLog.AddMessage($"{Owner.Name} was thrown and missed");
        }

        public override IClonable Clone()
        {
            return new ThrowableComponent(this);
        }

        public ThrowableComponent(ThrowableComponent original)
        {
            CloneMembers(original);
        }

        public void CloneMembers(ThrowableComponent original)
        {
            Range = original.Range;
        }
    }
}
