using System;
using UnityEngine;

namespace States
{
    using Model;
    using View;
    using Controller;

    //This state takes the model that is contained in the ModelContainer object, and allow us to browse it using
    //a controller and two views. Both views are on the same child. On renders the shop as icons, the other renders it
    //as text (messages). There is no event system, so the text is printed every frame.
    public class ShopBrowseState : MonoBehaviour
    {
        private ShopModel shopModel;

        private ShopController shopController;
        private ShopView shopView;
        private ShopMessageView shopMessageView;

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Start()
        //------------------------------------------------------------------------------------------------------------------------
        //This method gets the whole setup going
        protected void Start()
        {
            Initialize();  
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Initialize()
        //------------------------------------------------------------------------------------------------------------------------
        public void Initialize()
        {
            //Set up the model and controller
            shopModel = new ShopModel();
            shopController = new ShopController(shopModel);

            //get view from children
            shopView = GetComponentInChildren<ShopView>();
            Debug.Assert(shopView != null);

            //get mesageview from children
            shopMessageView = GetComponentInChildren<ShopMessageView>();
            Debug.Assert(shopMessageView != null);

            //setup model and controller
            Customer customer = new Customer();
            shopModel.SetCustomer(customer);

            //link them
            shopView.Initialize(shopModel, shopController);//view1
            shopMessageView.Initialize(shopModel);//view2
        }

    }
}
