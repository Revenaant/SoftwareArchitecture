using Model.Items;
using System;
using System.Collections.Generic;
using Utility;

namespace Model
{
    public abstract class TraderModel : ITrader, IObservable<TradeNotification>, IObserver<TradeNotification>
    {
        private IDisposable unsubscriber;
        protected List<IObserver<TradeNotification>> observers;

        public Inventory Inventory { get; protected set; }
        public string Name { get; protected set; }
        public int Gold { get; protected set; }

        protected TraderModel()
        {
            observers = new List<IObserver<TradeNotification>>();
        }

        ~TraderModel()
        {
            for (int i = 0; i < observers.Count; i++)
                observers[i].OnCompleted();
        }

        protected abstract void Initialize(string name, int startingGold, int inventoryCapacity);
        public abstract void Restock();

        public virtual void Sell(ITrader buyer)
        {
            ITradeable tradeable = Inventory.GetSelectedItem();

            if (buyer.Gold <= 0 || buyer.Gold < tradeable.Cost)
            {
                TradeLog.AddMessage($"Trader {buyer.Name} does not have enough gold to buy that!");
                TradeLog.AddMessage($"{buyer.Name}'s gold: {buyer.Gold}, item cost: {tradeable.Cost}");
                NotifyObservers(new TradeNotification(this, buyer, tradeable, isSuccessful: false));
                return;
            }

            if (--tradeable.Quantity <= 0)
                Inventory.Remove((Item)tradeable);

            AddGold(tradeable.Cost);
            NotifyObservers(new TradeNotification(this, buyer, tradeable, isSuccessful: true));
        }

        public virtual void OnItemBought(ITradeable tradeable, ITrader seller)
        {
            AddGold(-tradeable.Cost);
            Inventory.Add((Item)tradeable);

            TradeLog.AddMessage($"Trader {Name} just bought [{tradeable.Name}] for {tradeable.Cost} Gold from {seller.Name}!");
            TradeLog.AddMessage($"{Name}'s remaining gold: {Gold}");
        }

        protected void AddGold(int value)
        {
            Gold += value;
            Gold.Clamp(0, 99999);
        }

        protected void NotifyObservers(TradeNotification notification)
        {
            for (int i = 0; i < observers.Count; i++)
                observers[i].OnNext(notification);
        }

        public IDisposable Subscribe(IObserver<TradeNotification> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber<TradeNotification>(observers, observer);
        }

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
