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
        private InventoryView shopInventoryView;
        private InventoryView customerInventoryView;
        private ShopMessageView shopMessageView;

        private bool buying = true;

        public ShopBrowseState()
        {
            // Create shop
            ShopModel shopModel = new ShopModel();

            // Create controller
            shopController = new ShopController(shopModel);

            // Setup model and controller
            Customer customer = new Customer("Guy");
            shopModel.SetCustomer(customer);

            // Create shop view
            shopInventoryView = new InventoryView(shopModel.Inventory, shopController, System.Drawing.Color.LightGoldenrodYellow);
            shopInventoryView.SetViewScreenPosition(0.5f, 0.25f);
            AddChild(shopInventoryView);
            Helper.AlignToCenter(shopInventoryView, true, false);

            // Create customer inventory view
            customerInventoryView = new InventoryView(customer.Inventory, shopController, System.Drawing.Color.RosyBrown);
            customerInventoryView.SetViewScreenPosition(0.5f, 0.95f);
            AddChild(customerInventoryView);
            Helper.AlignToCenter(customerInventoryView, true, false);

            // Create message view
            shopMessageView = new ShopMessageView(shopModel);
            AddChild(shopMessageView);
            Helper.AlignToCenter(shopMessageView, true, false);
        }

        public void Step()
        {
            // TODO This kills MVC but temporarily easier.
            if (Input.GetKeyDown(Key.TAB))
            {
                buying = !buying;
            }

            if (buying)
                shopInventoryView.Step();
            else
                customerInventoryView.Step();
        }
    }
}
