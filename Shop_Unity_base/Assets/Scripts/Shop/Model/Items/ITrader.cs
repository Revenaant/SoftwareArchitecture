using System;

namespace Model.Items
{
    interface ITrader
    {
        void Buy(ITrader other, ITradeable tradeable);
        void Sell(ITrader other, ITradeable tradeable);
    }
}
