using UnityEngine;

public abstract class Treasure : BaseObject
{
    private PoolableObject _poolable;
    private bool _canInterection;

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

    protected override void Setting()
    {
        base.Setting();
        _canInterection = true;
    }

    protected override void Release()
    {
        _canInterection = false;
        base.Release();
    }

    protected abstract void Reward();

    public void Interection()
    {
        if (_canInterection == false) return;
        _canInterection = false;
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
