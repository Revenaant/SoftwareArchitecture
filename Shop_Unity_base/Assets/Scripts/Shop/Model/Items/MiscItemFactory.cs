namespace Model.Items
{
    using Utility;
    using StatsEffect = RPGStatsComponent.StatsEffect;

    /// <summary>
    /// This factory creates items that do not fit in any stereotype.
    /// The current version will return a fully randomly generated item
    /// </summary>
    public partial class MiscItemFactory : IItemFactory
    {
        private const int MAX_COST = 50;
        private const int MAX_QUANTITY = 5;
        private const int MAX_POTENCY = 50;
        private const float CHANCE_OF_CONSUMABLE = 0.8f;

        private string[] adjectives = { "Hot", "Cold", "Large", "Small", "Long", "Rancid", "Sparkly", "Dumb", "Sad", "Sweet" };
        private string[] nouns = { "Squash", "Cream", "Bucket", "Door", "Pants", "Box", "Sock", "Squid", "Plank", "Hand", "Glass",
                                    "Oil", "Fire", "Water", "Moon", "Cracker", "Key", "Eye", "Soap", "Cat", "Seed", "Fur", "Liquid" };

        public MiscItemFactory()
        {

        }

        Item IItemFactory.CreateRandom()
        {
            return GenerateRandomMiscItem();
        }

        public MiscItem GenerateRandomMiscItem()
        {
            string name = $"{adjectives[Random.Get(0, adjectives.Length)]} {nouns[Random.Get(0, nouns.Length)]}";
            int cost = Random.Get(1, MAX_COST);
            int quantity = Random.Get(1, MAX_QUANTITY);
            MiscItem item = new MiscItem(name, cost, quantity);

            if (CHANCE_OF_CONSUMABLE >= Random.Get(0.0f, 1.0f))
            {
                StatsEffect effect = (StatsEffect)Random.Get(0, RPGStatsComponent.StatsEffectCount);
                item.AddComponent(new ConsumableComponent(effect, Random.Get(-MAX_POTENCY, MAX_POTENCY)));
            }

            return item;
        }
    }
}