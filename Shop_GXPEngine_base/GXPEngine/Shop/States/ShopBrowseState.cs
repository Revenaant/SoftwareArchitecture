using System;
using GXPEngine;

namespace States
{
    using Model;
    using View;
    using Controller;
    using Model.Items;

    public class ShopBrowseState : GameObject
    {
        private GXPInputManager inputManager;

        private InventoryController shopController;
        private InventoryController customerController;
        private InventoryView shopInventoryView;
        private InventoryView customerInventoryView;
        private ShopMessageView shopMessageView;

        private bool buying = true;
        private InventoryController currentController
        {
            get
            {
                return buying
                  ? shopController
                  : customerController;
            }
        }

        private InventoryView currentView
        {
            get
            {
                return buying
                  ? shopInventoryView
                  : customerInventoryView;
            }
        }

        public ShopBrowseState()
        {
            // Create models
            ShopModel shopModel = new ShopModel();
            Customer customer = new Customer("Leonard");

            // Create controller
            shopController = new InventoryController(shopModel, customer);
            customerController = new InventoryController(customer, shopModel);
            inputManager = new GXPInputManager();

            // SetTrade
            shopModel.SetOtherTrader(customer);
            customer.SetOtherTrader(shopModel);

            // Create shop view
            shopInventoryView = new InventoryView(shopController, inputManager, System.Drawing.Color.LightGoldenrodYellow);
            shopInventoryView.SetViewScreenPosition(0.5f, 0.25f);
            AddChild(shopInventoryView);
            Helper.AlignToCenter(shopInventoryView, true, false);
            shopInventoryView.RegisterEvents(shopModel, customer);

            // Create customer inventory view
            customerInventoryView = new InventoryView(customerController, inputManager, System.Drawing.Color.RosyBrown);
            customerInventoryView.SetViewScreenPosition(0.5f, 0.95f);
            AddChild(customerInventoryView);
            Helper.AlignToCenter(customerInventoryView, true, false);
            customerInventoryView.RegisterEvents(customer, shopModel);

            // Create message view
            shopMessageView = new ShopMessageView(shopModel);
            AddChild(shopMessageView);
            Helper.AlignToCenter(shopMessageView, true, false);
            shopMessageView.RegisterEvents(shopModel, customer);
        }

        public void Step()
        {
            // TODO This kills MVC but temporarily easier.
            if (Input.GetKeyDown(Key.TAB))
                ToggleInventories();

            // currentController.Step()
            currentView.Step();
        }

        private void ToggleInventories()
        {
            buying = !buying;
        }
    }
}
