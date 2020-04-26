using System;
using GXPEngine;

namespace States
{
    using Model;
    using View;
    using Controller;

    public class ShopBrowseState : GameObject
    {
        private ShopController shopController;
        private ShopView shopView;
        private ShopMessageView shopMessageView;

        public ShopBrowseState()
        {
            // Create shop
            ShopModel shop = new ShopModel();

            // Create controller
            shopController = new ShopController(shop);

            // Create shop view
            shopView = new ShopView(shop, shopController);
            AddChild(shopView);
            Helper.AlignToCenter(shopView, true, true);

            // Create message view
            shopMessageView = new ShopMessageView(shop);
            AddChild(shopMessageView);
            Helper.AlignToCenter(shopMessageView, true, false);
        }

        public void Step()
        {
            shopView.Step();
            shopMessageView.Step();
        }
    }
}
