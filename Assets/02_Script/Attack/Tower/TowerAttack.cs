using UnityEngine;

public abstract class TowerAttack : Attack
{
    protected Enemy _target;
    protected PoolableObject _poolable;
    protected float _damage;


    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public virtual void SettingTarget(Enemy target, float musicPower)
    {
        _target = target;
        _damage = musicPower;
    }

    public virtual void SettingTarget(Vector3 target, float musicPower)
    {
        _damage = musicPower;
    }
}
