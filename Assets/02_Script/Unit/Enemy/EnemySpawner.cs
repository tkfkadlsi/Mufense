using UnityEngine;

public class EnemySpawner : BaseInit
{
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public void SpawnEnemy(PoolType type, int wayNumber)
    {
        if (type != PoolType.Null)
        {
            Managers.Instance.Pool.PopObject(type, transform.position).GetComponent<Enemy>().EnemySetting(wayNumber);
        }
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
