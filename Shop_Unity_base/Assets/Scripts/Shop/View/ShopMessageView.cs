namespace View
{
    using UnityEngine;
    using Model;
    using Model.Items;

    public class ShopMessageView : MonoBehaviour, IObserver<TradeNotification>
    {
        private ShopModel shopModel;

        // This method is used to initialize values, because we can't use a constructor.
        public void Initialize(ShopModel shopModel, ITrader otherTrader)
        {
            this.shopModel = shopModel;

            RegisterEvents(otherTrader);
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

        private void OnShopUpdated()
        {
            string[] messages = TradeLog.GetMessages();

            for (int i = 0; i < messages.Length; i++)
                Debug.Log(messages[i]);
        }

        void IObserver<TradeNotification>.OnNext(TradeNotification value)
        {
            OnShopUpdated();
        }

        void IObserver<TradeNotification>.OnCompleted()
        {
            unsubscriber.Dispose();
        }

        void IObserver<TradeNotification>.OnError(Exception error)
        {
            throw new NotSupportedException("There was an error sending data, this method should never be called");
        }
    }
}
