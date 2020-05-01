namespace Utility
{
    public static class Random
    {
        private static System.Random instance;
        public static System.Random Instance
        {
            get
            {
                if (instance == null)
                    instance = new System.Random();

                return instance;
            }
        }
    }
}
