using Utility;

namespace Model.Items
{
    public class Weapon : Item, IClonable
    {
        private int damage;
        private float criticalChance;
        private float accuracy;
        public int Damage => damage;

        public Weapon(string name, int cost, int damage, float accuracy, float criticalChance) : base(name, cost)
        {
            this.damage = damage;
            this.accuracy = accuracy;
            this.criticalChance = criticalChance;

            iconName = "itemWeapon";
        }

        public Weapon(Weapon weapon)
        {
            CloneFields(weapon);
        }

        protected override void CloneFields(Item item)
        {
            base.CloneFields(item);

            Weapon referenceWeapon = (Weapon)item;
            damage = referenceWeapon.damage;
            criticalChance = referenceWeapon.criticalChance;
            accuracy = referenceWeapon.accuracy;
        }

        public override IClonable Clone()
        {
            return new Weapon(this);
        }

        public void SetDamage(int value)
        {
            damage = value;
        }

        public void SetAccuracy(float value)
        {
            accuracy = value;
            accuracy.Clamp(0.0f, 1.0f);
        }

        /// <summary>
        /// Sets criticalChance to the given value, clamped between 0-1f
        /// </summary>
        /// <param name="value"></param>
        public void SetCriticalChance(float value)
        {
            criticalChance = value;
            criticalChance.Clamp(0.0f, 1.0f);
        }
    }
}
