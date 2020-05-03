namespace Model.Items
{
    using System;
    using System.Collections.Generic;

    public class WeaponFactory : IItemFactory
    {
        public enum WeaponType { Bow, Mace, Flail, Sword, Dagger, Battleaxe }

        private Dictionary<WeaponType, Weapon> TypeToPrototype = new Dictionary<WeaponType, Weapon>();

        public WeaponFactory()
        {
            TypeToPrototype.Add(WeaponType.Bow, new Weapon(name: "Bow", cost: 35, damage: 7, accuracy: 0.8f, criticalChance: 0.4f));
            TypeToPrototype.Add(WeaponType.Mace, new Weapon(name: "Mace", cost: 40, damage: 8, accuracy: 0.6f, criticalChance: 0.3f));
            TypeToPrototype.Add(WeaponType.Flail, new Weapon(name: "Flail", cost: 65, damage: 9, accuracy: 0.3f, criticalChance: 0.9f));
            TypeToPrototype.Add(WeaponType.Sword, new Weapon(name: "Sword", cost: 30, damage: 5, accuracy: 0.6f, criticalChance: 0.5f));
            TypeToPrototype.Add(WeaponType.Dagger, new Weapon(name: "Dagger", cost: 20, damage: 3, accuracy: 0.9f, criticalChance: 0.8f));
            TypeToPrototype.Add(WeaponType.Battleaxe, new Weapon(name: "Battleaxe", cost: 70, damage: 12, accuracy: 0.5f, criticalChance: 0.6f));
        }

        Item IItemFactory.CreateRandom()
        {
            return CreateRandomWeapon();
        }

        public Weapon CreateRandomWeapon()
        {
            int randomType = Utility.Random.Get(0, Enum.GetNames(typeof(WeaponType)).Length);
            return CreateWeapon((WeaponType)randomType);
        }

        public Weapon CreateWeapon(WeaponType weaponType)
        {
            return (Weapon)TypeToPrototype[weaponType].Clone();
        }

        public Weapon CreateBow() => (Weapon)TypeToPrototype[WeaponType.Bow].Clone();
        public Weapon CreateMace() => (Weapon)TypeToPrototype[WeaponType.Mace].Clone();
        public Weapon CreateFlail() => (Weapon)TypeToPrototype[WeaponType.Flail].Clone();
        public Weapon CreateSword() => (Weapon)TypeToPrototype[WeaponType.Sword].Clone();
        public Weapon CreateDagger() => (Weapon)TypeToPrototype[WeaponType.Dagger].Clone();
        public Weapon CreateBattleaxe() => (Weapon)TypeToPrototype[WeaponType.Battleaxe].Clone();
    }
}
