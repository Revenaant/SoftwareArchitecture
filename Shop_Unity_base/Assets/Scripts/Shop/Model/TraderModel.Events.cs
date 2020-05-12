namespace Model
{
    using Model.Items;
    using System;
    using System.Collections.Generic;

    public abstract partial class TraderModel : ITrader, IObservable<RedrawNotification>, IObservable<TradeNotification>, IObserver<TradeNotification>
    {
        private IDisposable unsubscriber;

        protected List<IObserver<TradeNotification>> tradeObservers;
        protected List<IObserver<RedrawNotification>> redrawObservers;


        //------------------------Observable<RedrawNotification>----------------------------

        protected void NotifyRedrawObservers(RedrawNotification notification)
        {
            for (int i = 0; i < redrawObservers.Count; i++)
                redrawObservers[i].OnNext(notification);
        }

        public IDisposable Subscribe(IObserver<RedrawNotification> observer)
        {
            if (!redrawObservers.Contains(observer))
                redrawObservers.Add(observer);

            return new Unsubscriber<RedrawNotification>(redrawObservers, observer);
        }


        //------------------------Observable<TradeNotification>----------------------------

        protected void NotifyTradeObservers(TradeNotification notification)
        {
            for (int i = 0; i < tradeObservers.Count; i++)
                tradeObservers[i].OnNext(notification);
        }

        public IDisposable Subscribe(IObserver<TradeNotification> observer)
        {
            if (!tradeObservers.Contains(observer))
                tradeObservers.Add(observer);

            return new Unsubscriber<TradeNotification>(tradeObservers, observer);
        }

        //------------------------Observer<TradeNotification>----------------------------

        public void SubscribeToObservable(IObservable<TradeNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        void IObserver<TradeNotification>.OnNext(TradeNotification tradeNotification)
        {
            if (tradeNotification.isSuccessful)
                OnItemBought(tradeNotification.tradedObject, tradeNotification.seller);
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
