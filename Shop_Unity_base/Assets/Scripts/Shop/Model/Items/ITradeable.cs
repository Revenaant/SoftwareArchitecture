namespace Model.Items
{
    public interface ITradeable
    {
        string Name { get; }

        // TODO place IconName in a different component
        string IconName { get; }
        int Cost { get; }
    }
}
