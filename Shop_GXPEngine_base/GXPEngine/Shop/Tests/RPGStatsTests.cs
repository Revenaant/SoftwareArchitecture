namespace Tests
{
    using Model;
    using NUnit.Framework;

    [TestFixture]
    class RPGStatsTests
    {
        private CustomerModel character;

        [SetUp]
        public void Initialize()
        {
            character = new CustomerModel("TestMan", 0);
            RPGStatsComponent stats = new RPGStatsComponent(100, 100, 100, 100);
            character.AddComponent(stats);
        }

        [Test]
        public void TestGetComponentIsNotNull()
        {
            RPGStatsComponent originalStatsComponent = character.GetComponent<RPGStatsComponent>();
            Assert.IsNotNull(originalStatsComponent);
        }

        [Test]
        public void TestCloneStats()
        {
            RPGStatsComponent originalStatsComponent = character.GetComponent<RPGStatsComponent>();
            RPGStatsComponent newStatsComponent = (RPGStatsComponent)originalStatsComponent.Clone();

            Assert.AreEqual(originalStatsComponent.Health, newStatsComponent.Health);
            Assert.AreEqual(originalStatsComponent.Mana, newStatsComponent.Mana);
            Assert.AreEqual(originalStatsComponent.Nourishment, newStatsComponent.Nourishment);
            Assert.AreEqual(originalStatsComponent.Strength, newStatsComponent.Strength);
        }

        [Test]
        public void TestCloneIsDeepCopy()
        {
            RPGStatsComponent originalStatsComponent = character.GetComponent<RPGStatsComponent>();
            RPGStatsComponent newStatsComponent = (RPGStatsComponent)originalStatsComponent.Clone();

            newStatsComponent.Consume(new ConsumableComponent(RPGStatsComponent.StatsEffect.HealthModifier, -30));
            newStatsComponent.Consume(new ConsumableComponent(RPGStatsComponent.StatsEffect.ManaModifier, 34));
            newStatsComponent.Consume(new ConsumableComponent(RPGStatsComponent.StatsEffect.NourishmentModifier, 10));
            newStatsComponent.Consume(new ConsumableComponent(RPGStatsComponent.StatsEffect.StrengthModifier, -50));

            Assert.AreNotSame(originalStatsComponent, newStatsComponent);
            Assert.AreNotEqual(originalStatsComponent.Health, newStatsComponent.Health);
            Assert.AreNotEqual(originalStatsComponent.Mana, newStatsComponent.Mana);
            Assert.AreNotEqual(originalStatsComponent.Nourishment, newStatsComponent.Nourishment);
            Assert.AreNotEqual(originalStatsComponent.Strength, newStatsComponent.Strength);
        }

        [TestCase(50, 150)]
        [TestCase(100, 200)]
        [TestCase(-75, 25)]
        public void TestConsumeModifiesHealth(int value, int expectedResult)
        {
            RPGStatsComponent stats = character.GetComponent<RPGStatsComponent>();
            ConsumableComponent consumable = new ConsumableComponent(RPGStatsComponent.StatsEffect.HealthModifier, value);
            stats.Consume(consumable);

            Assert.AreEqual(expectedResult, stats.Health);
        }

        [TestCase(50, 150)]
        [TestCase(100, 200)]
        [TestCase(-75, 25)]
        public void TestConsumeModifiesMana(int value, int expectedResult)
        {
            RPGStatsComponent stats = character.GetComponent<RPGStatsComponent>();
            ConsumableComponent consumable = new ConsumableComponent(RPGStatsComponent.StatsEffect.ManaModifier, value);
            stats.Consume(consumable);

            Assert.AreEqual(expectedResult, stats.Mana);
        }

        [TestCase(50, 150)]
        [TestCase(100, 200)]
        [TestCase(-75, 25)]
        public void TestConsumeModifiesNourishment(int value, int expectedResult)
        {
            RPGStatsComponent stats = character.GetComponent<RPGStatsComponent>();
            ConsumableComponent consumable = new ConsumableComponent(RPGStatsComponent.StatsEffect.NourishmentModifier, value);
            stats.Consume(consumable);

            Assert.AreEqual(expectedResult, stats.Nourishment);
        }

        [TestCase(50, 150)]
        [TestCase(100, 200)]
        [TestCase(-75, 25)]
        public void TestConsumeModifiesStrength(int value, int expectedResult)
        {
            RPGStatsComponent stats = character.GetComponent<RPGStatsComponent>();
            ConsumableComponent consumable = new ConsumableComponent(RPGStatsComponent.StatsEffect.StrengthModifier, value);
            stats.Consume(consumable);

            Assert.AreEqual(expectedResult, stats.Strength);
        }

        [TestCase(35, 65)]
        [TestCase(5, 95)]
        [TestCase(-30, 130)]
        public void TestOnHitSubtractsHealth(int value, int expectedResult)
        {
            RPGStatsComponent stats = character.GetComponent<RPGStatsComponent>();
            stats.OnHit(RPGStatsComponent.StatsEffect.HealthModifier, value);

            Assert.AreEqual(expectedResult, stats.Health);
        }
    }
}
