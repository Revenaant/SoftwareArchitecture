using Model.Items;
using System;
using System.Collections.Generic;
using Utility;

namespace Model
{
    public abstract partial class TraderModel : ITrader, IObservable<RedrawNotification>, IObservable<TradeNotification>, IObserver<TradeNotification>
    {
        public Inventory Inventory { get; protected set; }
        public string Name { get; protected set; }
        public int Gold { get; protected set; }

        protected TraderModel()
        {
            tradeObservers = new List<IObserver<TradeNotification>>();
            redrawObservers = new List<IObserver<RedrawNotification>>();
        }

        ~TraderModel()
        {
            for (int i = 0; i < tradeObservers.Count; i++)
                tradeObservers[i].OnCompleted();
            for (int i = 0; i < redrawObservers.Count; i++)
                redrawObservers[i].OnCompleted();
        }

        protected abstract void Initialize(string name, int startingGold, int inventoryCapacity);

        public abstract void Restock();

        public virtual void Sell(ITrader buyer)
        {
            ITradeable tradeable = Inventory.GetSelectedItem();
            if (tradeable == null)
                return;

            if (buyer.Gold <= 0 || buyer.Gold < tradeable.Cost)
            {
                NotifyRedrawObservers(new RedrawNotification($"Trader {buyer.Name} does not have enough gold to buy that! { buyer.Name }'s gold: {buyer.Gold}, item cost: {tradeable.Cost}"));
                NotifyTradeObservers(new TradeNotification(this, buyer, tradeable, isSuccessful: false));
                return;
            }

            if (--tradeable.Quantity <= 0)
                Inventory.Remove((Item)tradeable);

            AddGold(tradeable.Cost);
            NotifyTradeObservers(new TradeNotification(this, buyer, tradeable, isSuccessful: true));
        }

        public virtual void OnItemBought(ITradeable tradeable, ITrader seller)
        {
            AddGold(-tradeable.Cost);

            Item newItem = (Item)tradeable;
            Item existingItem = Inventory.GetItemByName(newItem.Name);

            // Add new Item or stack repetitions
            if (existingItem == null)
                Inventory.Add((Item)newItem.Clone());
            else
                existingItem.Quantity++;

            NotifyRedrawObservers(new RedrawNotification($"Trader {Name} just bought [{tradeable.Name}] for {tradeable.Cost} Gold from {seller.Name}!"));
        }

        protected void AddGold(int value)
        {
            Gold += value;
            Gold.Clamp(0, 99999);
        }
    }
}
