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

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopBrowseState()
        //------------------------------------------------------------------------------------------------------------------------
        public ShopBrowseState()
        {
            //create shop
            ShopModel shop = new ShopModel();

            //create controller
            shopController = new ShopController(shop);

            //create shop view
            shopView = new ShopView(shop, shopController);
            AddChild(shopView);
            Helper.AlignToCenter(shopView, true, true);

            //create message view
            shopMessageView = new ShopMessageView(shop);
            AddChild(shopMessageView);
            Helper.AlignToCenter(shopMessageView, true, false);

        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Step()
        //------------------------------------------------------------------------------------------------------------------------
        //update the views
        public void Step()
        {
            shopView.Step();
            shopMessageView.Step();
        }

    }
}
