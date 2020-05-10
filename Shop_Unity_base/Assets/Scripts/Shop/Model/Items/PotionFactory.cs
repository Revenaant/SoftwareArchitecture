namespace Model.Items
{
    using System;
    using System.Collections.Generic;

    public class PotionFactory : IItemFactory
    {
        public enum PotionType { HealtSmall, HealthGrand, Mana, Strength, Poison, Invisibility }

        private Dictionary<PotionType, Potion> TypeToPrototype = new Dictionary<PotionType, Potion>();

        public PotionFactory()
        {
            string namePrefix = "P";
            AddOrUpdatePrototype(PotionType.HealtSmall, new Potion(name: $"{namePrefix}-sHP", cost: 10),
                new ConsumableComponent(RPGStatsComponent.StatsEffect.HealthModifier, potency: 10));

            AddOrUpdatePrototype(PotionType.HealthGrand, new Potion(name: $"{namePrefix}-gHP", cost: 20),
                new ConsumableComponent(RPGStatsComponent.StatsEffect.HealthModifier, potency: 10, useCount: 2));

            AddOrUpdatePrototype(PotionType.Mana, new Potion(name: $"{namePrefix}-Mana", cost: 12),
                new ConsumableComponent(RPGStatsComponent.StatsEffect.ManaModifier, potency: 10));

            AddOrUpdatePrototype(PotionType.Strength, new Potion(name: $"{namePrefix}-Str", cost: 25),
                new ConsumableComponent(RPGStatsComponent.StatsEffect.StrengthModifier, potency: 2, duration: 15));

            AddOrUpdatePrototype(PotionType.Poison, new Potion(name: $"{namePrefix}-Poison", cost: 25),
                new ConsumableComponent(RPGStatsComponent.StatsEffect.HealthModifier, potency: -5, useCount: 3),
                new ThrowableComponent(range: 20));

            AddOrUpdatePrototype(PotionType.Invisibility, new Potion(name: $"{namePrefix}-Invis", cost: 30),
                new ConsumableComponent(RPGStatsComponent.StatsEffect.Invisibility, potency: 1, duration: 30));
        }

        public void AddOrUpdatePrototype(PotionType type, Potion potion, params Component<Item>[] components)
        {
            potion.AddComponents(components);
            TypeToPrototype.Add(type, potion);
        }

        Item IItemFactory.CreateRandom()
        {
            return CreateRandomPotion();
        }

        public Potion CreateRandomPotion()
        {
            int randomType = Utility.Random.Get(0, Enum.GetNames(typeof(PotionType)).Length);
            return CreatePotion((PotionType)randomType);
        }

        public Potion CreatePotion(PotionType potionType)
        {
            return (Potion)TypeToPrototype[potionType].Clone();
        }

        public Potion CreateSmallHealthPotion() => (Potion)TypeToPrototype[PotionType.HealtSmall].Clone();
        public Potion CreateGrandHealthPotion() => (Potion)TypeToPrototype[PotionType.HealthGrand].Clone();
        public Potion CreateManaPotion() => (Potion)TypeToPrototype[PotionType.Mana].Clone();
        public Potion CreateStrengthPotion() => (Potion)TypeToPrototype[PotionType.Strength].Clone();
        public Potion CreatePoisonPotion() => (Potion)TypeToPrototype[PotionType.Poison].Clone();
        public Potion CreateInvisibilityPotion() => (Potion)TypeToPrototype[PotionType.Invisibility].Clone();
    }
}
