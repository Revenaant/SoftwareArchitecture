﻿namespace Model
{
    using Utility;

    public class AttackerComponent : Component<Item>
    {
        private float accuracy;
        private float criticalChance;

        private int damage;
        public int Damage => damage;

        public AttackerComponent(int damage, float accuracy, float criticalChance)
        {
            this.damage = damage;
            this.accuracy = accuracy;
            this.criticalChance = criticalChance;
        }

        public void Attack(ITargetable target)
        {
            if (!GetHitRollResult())
                return;

            int totalDamage = Damage * (GetCriticalRollResult() ? 2 : 1);
            target.OnHit(RPGStatsComponent.StatsEffect.HealthModifier, totalDamage);
        }

        private bool GetHitRollResult()
        {
            float hitRoll = Random.Get(0, 1);
            return accuracy >= hitRoll;
        }

        private bool GetCriticalRollResult()
        {
            float critRoll = Random.Get(0, 1);
            return criticalChance >= critRoll;
        }

        public void SetDamage(int value)
        {
            damage = value;
            damage.Clamp(-999999, 999999);
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

        public AttackerComponent(AttackerComponent attackerComponent)
        {
            CloneMembers(attackerComponent);
        }

        private void CloneMembers(AttackerComponent attackerComponent)
        {
            accuracy = attackerComponent.accuracy;
            criticalChance = attackerComponent.criticalChance;
            damage = attackerComponent.damage;
        }

        public override IClonable Clone()
        {
            return new AttackerComponent(this);
        }
    }
}
