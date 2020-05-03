﻿namespace View
{
    using UnityEngine;
    using Model;
    using Model.Items;

    public class ShopMessageView : MonoBehaviour
    {
        private ShopModel shopModel;

        // This method is used to initialize values, because we can't use a constructor.
        public void Initialize(ShopModel shopModel, ITrader otherTrader)
        {
            this.shopModel = shopModel;

            RegisterEvents(otherTrader);
        }

        private void RegisterEvents(ITrader trader)
        {
            trader.OnItemSoldEvent += OnShopUpdated;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Update()
        //------------------------------------------------------------------------------------------------------------------------        
        // This method polls the shop for messages and prints them. Since the shop caches the messages, it prints the same
        // message each frame. An event system would work better.

        private void OnShopUpdated(ITradeable tradeable, ITrader trader)
        {
            string[] messages = shopModel.GetMessages();

            for (int i = 0; i < messages.Length; i++)
                Debug.Log(messages[i]);
        }
    }
}
