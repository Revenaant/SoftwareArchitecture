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

        private TraderModel shopModel;
        private TraderModel customerModel;

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
            Initialize();
        }

        private void Initialize()
        {
            CreateModels();
            CreateControllers();
            CreateViews();
            RegisterObservers();
        }

        private void CreateModels()
        {
            shopModel = new ShopModel();
            customerModel = new CustomerModel("Leonard", 300);
        }

        private void CreateControllers()
        {
            shopController = new InventoryController(shopModel, customerModel);
            customerController = new InventoryController(customerModel, shopModel);
            inputManager = new GXPInputManager();
        }

        private void CreateViews()
        {
            // Create shop view
            shopInventoryView = new InventoryView(shopController, inputManager, System.Drawing.Color.LightGoldenrodYellow);
            shopInventoryView.SetViewScreenPosition(0.5f, 0.25f);
            AddChild(shopInventoryView);
            Helper.AlignToCenter(shopInventoryView, true, false);

            // Create customer inventory view
            customerInventoryView = new InventoryView(customerController, inputManager, System.Drawing.Color.RosyBrown);
            customerInventoryView.SetViewScreenPosition(0.5f, 0.95f);
            AddChild(customerInventoryView);
            Helper.AlignToCenter(customerInventoryView, true, false);

            // Create message view
            shopMessageView = new ShopMessageView();
            AddChild(shopMessageView);
            Helper.AlignToCenter(shopMessageView, true, false);
        }

        private void RegisterObservers()
        {
            shopModel.SubscribeToObservable(customerModel);
            customerModel.SubscribeToObservable(shopModel);
            RegisterViewsToTradeObservables(customerModel);
            RegisterViewsToTradeObservables(shopModel);
        }

        private void RegisterViewsToTradeObservables(IObservable<TradeNotification> observable)
        {
            shopInventoryView.SubscribeToObservable(observable);
            customerInventoryView.SubscribeToObservable(observable);
            shopMessageView.SubscribeToObservable(observable);
        }

        public void Step()
        {
            // TODO This kills MVC but temporarily easier.
            if (Input.GetKeyDown(Key.TAB))
                ToggleInventories();

            currentView.Step();
            inputManager.Update(currentController);
        }

        private void ToggleInventories()
        {
            buying = !buying;
        }
    }
}
