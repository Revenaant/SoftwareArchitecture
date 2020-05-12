namespace Model
{
    using System.Collections.Generic;

    public static class TradeLog
    {
        private const int MAX_MESSAGES_IN_QUEUE = 4;
        private static Queue<string> messages = new Queue<string>();

        // Returns the cached list of messages
        public static string[] GetMessages()
        {
            string[] queuedMessages = messages.ToArray();
            messages.Clear();

            return queuedMessages;
        }

        // Adds a message to the cache, cleaning it up if the limit is exceeded
        public static void AddMessage(string message)
        {
            messages.Enqueue(message);

            while (messages.Count > MAX_MESSAGES_IN_QUEUE)
                messages.Dequeue();
        }
    }
}
