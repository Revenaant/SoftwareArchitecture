namespace Model
{
    using System;

    public partial class RPGStatsComponent
    {
        private static readonly int statsEffectCount = Enum.GetNames(typeof(StatsEffect)).Length;
        public static int StatsEffectCount => statsEffectCount;

        public enum StatsEffect
        {
            NourishmentModifier,
            HealthModifier,
            ManaModifier,
            StrengthModifier,
            Invisibility
        }
    }
}
