namespace Model
{
    using Model.Items;

    public struct RedrawNotification
    {
        public readonly string message;

        public RedrawNotification(string message)
        {
            this.message = message;
        }
    }
}
