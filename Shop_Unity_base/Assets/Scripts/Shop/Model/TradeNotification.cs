namespace Model
{
    using Model.Items;

    public struct TradeNotification
    {
        public readonly ITrader seller;
        public readonly ITrader buyer;
        public readonly ITradeable tradedObject;
        public readonly bool isSuccessful;

        public TradeNotification(ITrader seller, ITrader buyer, ITradeable tradedObject, bool isSuccessful)
        {
            this.seller = seller;
            this.buyer = buyer;
            this.tradedObject = tradedObject;
            this.isSuccessful = isSuccessful;
        }
    }
}
