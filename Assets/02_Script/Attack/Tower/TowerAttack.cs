using UnityEngine;

public abstract class TowerAttack : Attack
{
    protected Transform _target;
    protected PoolableObject _poolable;
    protected float _musicPower;


    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public virtual void SettingTarget(Transform target, float musicPower)
    {
        _target = target;
        _musicPower = musicPower;
    }
}
