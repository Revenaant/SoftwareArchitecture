namespace View
{
    using System;
    using UnityEngine;
    using Model;
    using Model.Items;

    public class ShopMessageView : MonoBehaviour, IObserver<TradeNotification>
    {
        private IDisposable unsubscriber;

        public void SubscribeToObservable(IObservable<TradeNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

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
