namespace Model.Items
{
    public interface ITradeable
    {
        string Name { get; }
        int Cost { get; }
        int Quantity { get; set; }
    }
}
