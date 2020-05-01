using System;
using UnityEngine;

namespace States
{
    using Model;
    using View;
    using Controller;

    // This state takes the model that is contained in the ModelContainer object, and allow us to browse it using
    // a controller and two views. Both views are on the same child. On renders the shop as icons, the other renders it
    // as text (messages). There is no event system, so the text is printed every frame.
    public class ShopBrowseState : MonoBehaviour
    {
        private ShopModel shopModel;
        private ShopController shopController;

        private ShopView shopView;
        private ShopMessageView shopMessageView;

        // This method gets the whole setup going
        protected void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Set up the model and controller
            shopModel = new ShopModel();
            shopController = new ShopController(shopModel);

            // Get view from children
            shopView = GetComponentInChildren<ShopView>();
            Debug.Assert(shopView != null);

            // Get mesageview from children
            shopMessageView = GetComponentInChildren<ShopMessageView>();
            Debug.Assert(shopMessageView != null);

            // Setup model and controller
            Customer customer = new Customer("Leonard");
            shopModel.SetCustomer(customer);

            // Link them
            shopView.Initialize(shopModel, shopController);
            shopMessageView.Initialize(shopModel);
        }

    }
}
