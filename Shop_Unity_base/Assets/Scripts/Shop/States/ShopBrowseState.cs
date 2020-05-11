using System;
using UnityEngine;

namespace States
{
    using Model;
    using View;
    using Controller;

    public class ShopBrowseState : MonoBehaviour
    {
        [Header("Views")]

        [SerializeField]
        private InventoryView shopInventoryView;

        [SerializeField]
        private InventoryView customerInventoryView;

        [SerializeField]
        private ShopMessageView shopMessageView;

        private InventoryController shopController;
        private InventoryController customerController;
        private UnityInputManager inputManager;
        private TraderModel shopModel;
        private TraderModel customerModel;

        private bool buying = true;

        private InventoryView currentView
        {
            get
            {
                return buying
                  ? shopInventoryView
                  : customerInventoryView;
            }
        }

        // This method gets the whole setup going
        protected void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            CreateModels();
            CreateControllers();
            SetupViews();
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
            inputManager = new UnityInputManager();
        }

        private void SetupViews()
        {
            shopInventoryView.Initialize(shopController, inputManager);
            customerInventoryView.Initialize(customerController, inputManager);
        }
    }
}
