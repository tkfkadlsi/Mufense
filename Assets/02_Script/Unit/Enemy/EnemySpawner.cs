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

    public Enemy SpawnEnemy(PoolType type)
    {
        return Managers.Instance.Pool.PopObject(type, transform.position).GetComponent<Enemy>();
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
