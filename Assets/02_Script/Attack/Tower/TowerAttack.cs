using UnityEngine;

public abstract class TowerAttack : Attack
{
    protected Enemy _target;
    protected PoolableObject _poolable;
    protected float _damage;
    protected Tower _attacker;


    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public virtual void SettingTarget(Enemy target, float musicPower, Tower attacker)
    {
        _target = target;
        _damage = musicPower;
        _attacker = attacker;
    }

    public virtual void SettingTarget(Vector3 target, float musicPower, Tower attacker)
    {
        _damage = musicPower;
        _attacker = attacker;
    }
}
