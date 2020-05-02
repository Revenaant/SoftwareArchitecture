
namespace Model.Items
{
    using System;
    using System.Collections.Generic;

    public class PotionFactory : IItemFactory
    {
        public enum PotionType { Health, Mana, Strength, Defence, Invisibility }

        private Dictionary<PotionType, Potion> TypeToPrototype = new Dictionary<PotionType, Potion>();

        public PotionFactory()
        {
            string namePrefix = "P";
            TypeToPrototype.Add(PotionType.Health, new Potion(name: $"{namePrefix}HP", cost: 10, useAmount: 2));
            TypeToPrototype.Add(PotionType.Mana, new Potion(name: $"{namePrefix}Mana", cost: 12, useAmount: 3));
            TypeToPrototype.Add(PotionType.Strength, new Potion(name: $"{namePrefix}Str", cost: 25, useAmount: 1));
            TypeToPrototype.Add(PotionType.Defence, new Potion(name: $"{namePrefix}Def", cost: 25, useAmount: 1));
            TypeToPrototype.Add(PotionType.Invisibility, new Potion(name: $"{namePrefix}Invis", cost: 30, useAmount: 1));
        }

        Item IItemFactory.CreateRandom()
        {
            return CreateRandomPotion();
        }

        public Potion CreateRandomPotion()
        {
            int randomType = Utility.Random.Instance.Next(0, Enum.GetNames(typeof(PotionType)).Length);
            return CreatePotion((PotionType)randomType);
        }

        public Potion CreatePotion(PotionType potionType)
        {
            return (Potion)TypeToPrototype[potionType].Clone();
        }

        public Potion CreateHealthPotion() => (Potion)TypeToPrototype[PotionType.Health].Clone();
        public Potion CreateManaPotion() => (Potion)TypeToPrototype[PotionType.Mana].Clone();
        public Potion CreateStrengthPotion() => (Potion)TypeToPrototype[PotionType.Strength].Clone();
        public Potion CreateDefencePotion() => (Potion)TypeToPrototype[PotionType.Defence].Clone();
        public Potion CreateInvisibilityPotion() => (Potion)TypeToPrototype[PotionType.Invisibility].Clone();
    }
}
