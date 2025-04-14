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
                case EnemyType.None_0:
                    return PoolType.Null;
                case EnemyType.Normal_1:
                    return PoolType.Enemy;
                case EnemyType.Blink_2:
                    return PoolType.BlinkEnemy;
                case EnemyType.Cancled_3:
                    return PoolType.CancledEnemy;
            }

            return PoolType.Null;
        }
    }
}
