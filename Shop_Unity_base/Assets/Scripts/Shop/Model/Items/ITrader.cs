namespace Model.Items
{
    public interface ITrader
    {
        event Events.SellDelegate OnItemSoldEvent;

        string Name { get; }
        int Gold { get; }
        Inventory Inventory { get; }

        void Sell(ITrader buyer);
        void OnItemBought(ITradeable tradeable, ITrader seller);
        void Restock();
    }
}
