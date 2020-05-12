namespace Model.Items
{
    public interface ITrader
    {
        string Name { get; }
        int Gold { get; }
        Inventory Inventory { get; }

        void Sell(ITrader buyer);
        void OnItemBought(ITradeable tradeable, ITrader seller);
        void Restock();
    }
}
