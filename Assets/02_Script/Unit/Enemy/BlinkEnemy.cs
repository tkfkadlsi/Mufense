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

        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        float distance = Vector3.Distance(_core.transform.position, transform.position);

        transform.position = direction.normalized * distance;
    }
}