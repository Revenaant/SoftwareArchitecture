namespace Model
{
    public interface ITargetable
    {
        void OnHit(RPGStatsComponent.StatsEffect effect, int effectValue, float duration = 0);
    }
}
