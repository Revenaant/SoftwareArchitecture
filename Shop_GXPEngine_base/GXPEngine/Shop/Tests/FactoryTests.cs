namespace Tests
{
    using Model;
    using Model.Items;
    using NUnit.Framework;

    [TestFixture]
    class FactoryTests
    {
        private MiscItemFactory miscItemFactory;
        private WeaponFactory weaponFactory;
        private PotionFactory potionFactory;

        [SetUp]
        public void Initialize()
        {
            miscItemFactory = new MiscItemFactory();
            weaponFactory = new WeaponFactory();
            potionFactory = new PotionFactory();
        }

        [TestCase]
        public void TestFactoryReturnsIsNotNull()
        {
            IItemFactory factory = miscItemFactory;
            Item item = factory.CreateRandom();
            Assert.IsNotNull(item);

            factory = weaponFactory;
            item = factory.CreateRandom();
            Assert.IsNotNull(item);

            factory = potionFactory;
            item = factory.CreateRandom();
            Assert.IsNotNull(item);
        }

        public void TestFactoryPrototypeComponentIsNotNull()
        {
            Potion prototype = new Potion("TestPotion", 99, 1);
            prototype.AddComponent(new ConsumableComponent(RPGStatsComponent.StatsEffect.ManaModifier, 15));

            potionFactory.AddOrUpdatePrototype(PotionFactory.PotionType.Mana, prototype);
            Potion newPotion = potionFactory.CreateManaPotion();

            Assert.IsNotNull(newPotion.GetComponent<ConsumableComponent>());
        }

        [Test]
        public void TestFactoryCreatesPrototypeItemCorrectly()
        {
            Potion prototype = new Potion("TestPotion", 99, 1);
            prototype.AddComponent(new ConsumableComponent(RPGStatsComponent.StatsEffect.ManaModifier, 15));

            potionFactory.AddOrUpdatePrototype(PotionFactory.PotionType.Mana, prototype);
            Potion newPotion = potionFactory.CreateManaPotion();

            Assert.AreEqual(prototype.Name, newPotion.Name);
            Assert.AreEqual(prototype.Cost, newPotion.Cost);

            Assert.AreEqual(prototype.GetComponent<ConsumableComponent>().Effect, newPotion.GetComponent<ConsumableComponent>().Effect);
            Assert.AreEqual(prototype.GetComponent<ConsumableComponent>().Potency, newPotion.GetComponent<ConsumableComponent>().Potency);
        }
    }
}
