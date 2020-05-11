namespace Model
{
    public class ConsumableComponent : Component<Item>
    {
        private int useCount;

        public RPGStatsComponent.StatsEffect Effect { get; private set; }
        public int Potency { get; private set; }
        public float Duration { get; private set; }
        public bool IsSpent => (useCount <= 0);

        /// <summary>
        /// Creates a new Consubmable component
        /// </summary>
        /// <param name="potency">The strength of the item's effect. A value of 0 is used for effects that don't get multiplied</param>
        /// <param name="duration">The duration of the effect in seconds. A value of 0 is instantaneous</param>
        /// <param name="useCount">How many times can this be used before being spent</param>
        public ConsumableComponent(RPGStatsComponent.StatsEffect effect, int potency, float duration = 0, int useCount = 1)
        {
            Effect = effect;
            Potency = potency;
            Duration = duration;
            this.useCount = useCount;
        }

        private ConsumableComponent(ConsumableComponent original)
        {
            CloneMembers(original);
        }

        private void CloneMembers(ConsumableComponent original)
        {
            Effect = original.Effect;
            Potency = original.Potency;
            Duration = original.Duration;
            useCount = original.useCount;
        }

        public void Consume(RPGStatsComponent stats)
        {
            useCount--;

            bool isAboveZero = (useCount > 0);
            SetEnabled(isAboveZero);

            if (!isAboveZero)
                return;

            stats.Consume(this);
        }

        public override IClonable Clone()
        {
            return new ConsumableComponent(this);
        }
    }
}
