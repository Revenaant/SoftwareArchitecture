namespace Model
{
    using Utility;

    public partial class RPGStatsComponent : CustomComponent<CustomerModel>, ITargetable
    {
        private int health;
        public int Health => health;

        private int mana;
        public int Mana => mana;

        private int nourishment;
        public int Nourishment => nourishment;

        private int strength;
        public int Strength => strength;

        public RPGStatsComponent(int health, int mana, int nourishment, int strength)
        {
            this.health = health;
            this.mana = mana;
            this.nourishment = nourishment;
            this.strength = strength;
        }

        public void OnHit(StatsEffect effect, int effectValue, float duration = 0)
        {
            // Negate the value so it damages by default
            OnEffect(effect, -effectValue, duration);
        }

        public void Consume(ConsumableComponent consumable)
        {
            consumable.OnConsumed(this);
            OnEffect(consumable.Effect, consumable.Potency, consumable.Duration);
        }

        private void OnEffect(StatsEffect effect, int effectValue, float duration = 0)
        {
            switch (effect)
            {
                case StatsEffect.HealthModifier:
                    AddHealth(effectValue);
                    break;
                case StatsEffect.ManaModifier:
                    AddMana(effectValue);
                    break;
                case StatsEffect.NourishmentModifier:
                    AddNourishment(effectValue);
                    break;
                case StatsEffect.StrengthModifier:
                    AddStrength(effectValue, duration);
                    break;
                case StatsEffect.Invisibility:
                    SetInvisible(duration);
                    break;
                default:
                    throw new System.NotSupportedException("The effect of the consumable cannot be determined");
            }
        }

        // Extra parameters may be added to these functions to handle effects 
        // that apply over time or any other additional logic

        private void AddHealth(int value)
        {
            ClampToStandardValues(health += value);
        }

        private void AddMana(int value)
        {
            ClampToStandardValues(mana += value);
        }

        private void AddNourishment(int value)
        {
            ClampToStandardValues(nourishment += value);
        }

        private void AddStrength(int value, float duration)
        {
            ClampToStandardValues(strength += value);
        }

        private void SetInvisible(float duration)
        {
            // Handle hypotetical invisibility
        }

        private int ClampToStandardValues(int value)
        {
            return value.Clamp(0, 999999);
        }

        public override IClonable Clone()
        {
            return new RPGStatsComponent(this);
        }

        private RPGStatsComponent(RPGStatsComponent original)
        {
            CloneMembers(original);
        }

        private void CloneMembers(RPGStatsComponent original)
        {
            health = original.health;
            mana = original.mana;
            nourishment = original.nourishment;
            strength = original.strength;
        }
    }
}
