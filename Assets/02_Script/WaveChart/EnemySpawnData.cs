[System.Serializable]
public class EnemySpawnData
{
    public EnemyType EnemyType;

    public PoolType GetEnemyType
    {
        get
        {
            switch (EnemyType)
            {
                case EnemyType.None:
                    return PoolType.Null;
                case EnemyType.Normal:
                    return PoolType.Enemy;
                case EnemyType.Blink:
                    return PoolType.BlinkEnemy;
                case EnemyType.Cancled:
                    return PoolType.CancledEnemy;
            }

            return PoolType.Null;
        }
    }
}
