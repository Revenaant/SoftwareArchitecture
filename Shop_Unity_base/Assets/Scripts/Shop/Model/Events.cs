namespace Model
{
    public class Events
    {
        public delegate void BuyDelegate(Item item, Customer customer);
        public delegate void SellDelegate(Item item, Customer customer);
    }
}
