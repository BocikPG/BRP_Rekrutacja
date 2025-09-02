public static class GameEvents
{
    public delegate void OnEnemyKilled(IEnemy enemy);
    public static OnEnemyKilled EnemyKilled;

    public delegate void OnSoulUsed(SoulItem soul);
    public static OnSoulUsed SoulUsed;
}

