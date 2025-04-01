using System;
using System.Collections.Generic;


[Serializable]
public class EnemyDataInWave
{
    public EnemyType EnemyType;
    public int WayNumber;
    public PoolType PoolType
    {
        get
        {
            switch(EnemyType)
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
