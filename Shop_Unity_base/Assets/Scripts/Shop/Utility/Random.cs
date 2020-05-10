namespace Utility
{
    public static class Random
    {
        static private System.Random random = new System.Random();

        /// <summary>
        /// Gets a random value between the specified min (inclusive) and max (exclusive).
        /// </summary>
        public static int Get(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// Gets a random value between the specified min (inclusive) and max (exclusive).
        /// </summary>
        public static float Get(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
