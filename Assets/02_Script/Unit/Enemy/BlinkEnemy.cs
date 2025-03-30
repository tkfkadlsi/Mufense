using UnityEngine;

public class BlinkEnemy : Enemy
{
    protected override void Setting()
    {
        base.Setting();

        HP = 4 + Managers.Instance.Game.FindBaseInitScript<GameTimer>().EnemyHPLevel * 2;
        HPSlider.Slider.value = HP;
        HPSlider.Slider.maxValue = HP;
    }

    public override void Hit(float damage, int debuff = 0, Tower attacker = null)
    {
        base.Hit(damage, debuff);

        Managers.Instance.Pool.PopObject(PoolType.BlinkEffect, transform.position);

        BackBlink(Random.Range(-2, 3));
    }
}