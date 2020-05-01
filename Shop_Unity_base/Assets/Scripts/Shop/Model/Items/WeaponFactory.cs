namespace Model.Items
{
    using System;

    public class WeaponFactory : IItemFactory
    {
        public enum WeaponType { Bow, Mace, Flail, Sword, Dagger, Battleaxe }

        private Weapon defaultBow;
        private Weapon defaultMace;
        private Weapon defaultFlail;
        private Weapon defaultSword;
        private Weapon defaultDagger;
        private Weapon defaultBattleaxe;

        public WeaponFactory()
        {
            defaultBow = new Weapon(name: "Bow", cost: 35, damage: 7, accuracy: 0.8f, criticalChance: 0.4f);
            defaultMace = new Weapon(name: "Mace", cost: 40, damage: 8, accuracy: 0.6f, criticalChance: 0.3f);
            defaultFlail = new Weapon(name: "Flail", cost: 65, damage: 9, accuracy: 0.3f, criticalChance: 0.9f);
            defaultSword = new Weapon(name: "Sword", cost: 30, damage: 5, accuracy: 0.6f, criticalChance: 0.5f);
            defaultDagger = new Weapon(name: "Dagger", cost: 20, damage: 3, accuracy: 0.9f, criticalChance: 0.8f);
            defaultBattleaxe = new Weapon(name: "Battleaxe", cost: 70, damage: 12, accuracy: 0.5f, criticalChance: 0.6f);
        }

        Item IItemFactory.CreateRandom()
        {
            return CreateRandomWeapon();
        }

        public Weapon CreateRandomWeapon()
        {
            int randomType = Utility.Random.Instance.Next(0, Enum.GetNames(typeof(WeaponType)).Length);
            return CreateWeapon((WeaponType)randomType);
        }

        public Weapon CreateWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Bow:
                    return (Weapon)defaultBow.Clone();
                case WeaponType.Mace:
                    return (Weapon)defaultMace.Clone();
                case WeaponType.Flail:
                    return (Weapon)defaultFlail.Clone();
                case WeaponType.Sword:
                    return (Weapon)defaultSword.Clone();
                case WeaponType.Dagger:
                    return (Weapon)defaultDagger.Clone();
                case WeaponType.Battleaxe:
                    return (Weapon)defaultBattleaxe.Clone();
                default:
                    throw new NotSupportedException("WeaponType not know or supported");
            }
        }

        public Weapon CreateBow() => (Weapon)defaultBow.Clone();
        public Weapon CreateMace() => (Weapon)defaultMace.Clone();
        public Weapon CreateFlail() => (Weapon)defaultFlail.Clone();
        public Weapon CreateSword() => (Weapon)defaultSword.Clone();
        public Weapon CreateDagger() => (Weapon)defaultDagger.Clone();
        public Weapon CreateBattleaxe() => (Weapon)defaultBattleaxe.Clone();
    }
}
