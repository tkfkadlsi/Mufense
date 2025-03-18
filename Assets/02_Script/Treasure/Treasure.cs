using UnityEngine;

public abstract class Treasure : BaseObject
{
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Treasure;

        return true;
    }

    public void Reward()
    {

    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
