using System;
using UnityEngine;

namespace States
{
    using Controller;
    using Model;
    using View;

    public class ShopBrowseState : MonoBehaviour
    {
        private const int BASE_STATS_VALUE = 100;

        [Header("Views")]

        [SerializeField]
        private InventoryView shopInventoryView = null;

        [SerializeField]
        private InventoryView customerInventoryView = null;

        [SerializeField]
        private ShopMessageView shopMessageView = null;

        private InventoryController shopController;
        private InventoryController customerController;
        private UnityInputManager inputManager;
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

        protected void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            CreateModels();
            CreateControllers();
            SetupViews();
            RegisterObservers();
        }

        private void CreateModels()
        {
            shopModel = new ShopModel();

            CustomerModel newCustomer = new CustomerModel("Leonard", 300);
            newCustomer.AddComponent(new RPGStatsComponent(health: BASE_STATS_VALUE,
                mana: BASE_STATS_VALUE, nourishment: BASE_STATS_VALUE, strength: BASE_STATS_VALUE));

            customerModel = newCustomer;
        }

        private void CreateControllers()
        {
            shopController = new InventoryController(shopModel, customerModel);
            customerController = new InventoryController(customerModel, shopModel);
            inputManager = new UnityInputManager();
        }

        private void SetupViews()
        {
            shopInventoryView.Initialize(shopController, inputManager);
            customerInventoryView.Initialize(customerController, inputManager);
        }

        private void RegisterObservers()
        {
            shopModel.SubscribeToObservable(customerModel);
            customerModel.SubscribeToObservable(shopModel);

            RegisterViewsToRedrawObservables(customerModel);
            RegisterViewsToRedrawObservables(shopModel);
            RegisterViewsToRedrawObservables(customerController);
            RegisterViewsToRedrawObservables(shopController);
        }

        private void RegisterViewsToRedrawObservables(IObservable<RedrawNotification> observable)
        {
            shopInventoryView.SubscribeToObservable(observable);
            customerInventoryView.SubscribeToObservable(observable);
            shopMessageView.SubscribeToObservable(observable);
        }

        private void Update()
        {
            inputManager.Update(currentController);
        }
    }
}
