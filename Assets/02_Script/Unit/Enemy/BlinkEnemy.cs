using UnityEngine;

public class BlinkEnemy : Enemy
{
    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _moveCooltime = 4;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        HP = 14 + HPLevel * 7;
        HPSlider.SetMaxValue(HP);
    }

    public override void Hit(float damage, int debuff = 0, Tower attacker = null)
    {
        base.Hit(damage, debuff);

        Managers.Instance.Pool.PopObject(PoolType.BlinkEffect, transform.position);

        Jump(2);
    }
}