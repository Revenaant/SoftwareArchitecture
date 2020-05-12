namespace Tests
{
    using Model;
    using Model.Items;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    class ShopTest
    {
        private ITrader shop;
        private ITrader shopper;

        [SetUp]
        public void Initialize()
        {
            shop = new ShopModel();
            shopper = new CustomerModel("TestMan", 100);
        }

        [TestCase(1, 1001)]
        [TestCase(30, 1030)]
        public void TestTraderGainsGoldOnSell(int itemCost, int expectedShopGold)
        {
            Item item = new MiscItem("testItem", itemCost);
            shop.Inventory.Add(item);
            shop.Inventory.SelectItem(item);
            shop.Sell(shopper);

            Assert.AreEqual(shop.Gold, expectedShopGold);
        }

        [TestCase(1, 99)]
        [TestCase(50, 50)]
        [TestCase(200, -100)]
        public void TestTraderPaysGoldOnItemBought(int itemCost, int expectedShopperGold)
        {
            Item item = new MiscItem("testItem", itemCost);
            shopper.OnItemBought(item, shop);

            Assert.AreEqual(shopper.Gold, expectedShopperGold);
        }

        [TestCase(-1)]
        [TestCase(100)]
        public void TestGetItemOutOfRange(int index)
        {
            Assert.DoesNotThrow(() => shop.Inventory.GetItemByIndex(index), "Fail", Throws.TypeOf<System.IndexOutOfRangeException>());
        }

        [TestCase(1000)]
        public void TestTraderCannotAffortItem(int cost)
        {
            Item item = new MiscItem("testItem", cost);
            Assert.Less(shopper.Gold, cost);
        }

        [Test]
        public void TestCustomerRestockAddsGold()
        {
            int goldBefore = shopper.Gold;
            shopper.Restock();

            Assert.Greater(shopper.Gold, goldBefore);
        }

        [Test]
        public void TestShopItemsDifferentOnRestock()
        {
            List<Item> items = new List<Item>(shop.Inventory.Items);
            shop.Restock();
            List<Item> newItems = shop.Inventory.Items;

            Assert.AreNotSame(items, newItems);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreNotSame(items[i], newItems[i]);
            }
        }
    }
}
