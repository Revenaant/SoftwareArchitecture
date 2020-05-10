
namespace Model.Items
{
    using System;

    public class PotionFactory : IItemFactory
    {
        public enum PotionType { Health, Mana, Strength, Defence, Invisibility }

        private Potion defaultHealth;
        private Potion defaultMana;
        private Potion defaultStrength;
        private Potion defaultDefence;
        private Potion defaultInvisibility;

        public PotionFactory()
        {
            string namePrefix = "P";
            defaultHealth = new Potion(name: $"{namePrefix}HP", cost: 10, useAmount: 2);
            defaultMana = new Potion(name: $"{namePrefix}Mana", cost: 12, useAmount: 3);
            defaultStrength = new Potion(name: $"{namePrefix}Str", cost: 25, useAmount: 1);
            defaultDefence = new Potion(name: $"{namePrefix}Def", cost: 25, useAmount: 1);
            defaultInvisibility = new Potion(name: $"{namePrefix}Invis", cost: 30, useAmount: 1);
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
            switch (potionType)
            {
                case PotionType.Health:
                    return (Potion)defaultHealth.Clone();
                case PotionType.Mana:
                    return (Potion)defaultMana.Clone();
                case PotionType.Strength:
                    return (Potion)defaultStrength.Clone();
                case PotionType.Defence:
                    return (Potion)defaultDefence.Clone();
                case PotionType.Invisibility:
                    return (Potion)defaultInvisibility.Clone();
                default:
                    throw new NotSupportedException("WeaponType not know or supported");
            }
        }

        public Potion CreateHealthPotion() => (Potion)defaultHealth.Clone();
        public Potion CreateManaPotion() => (Potion)defaultMana.Clone();
        public Potion CreateStrengthPotion() => (Potion)defaultStrength.Clone();
        public Potion CreateDefencePotion() => (Potion)defaultDefence.Clone();
        public Potion CreateInvisibilityPotion() => (Potion)defaultInvisibility.Clone();
    }
}
