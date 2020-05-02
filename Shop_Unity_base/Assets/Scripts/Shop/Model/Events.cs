using Model.Items;

namespace Model
{
    public class Events
    {
        public delegate void SellDelegate(ITradeable tradeable, ITrader trader);
    }
}
