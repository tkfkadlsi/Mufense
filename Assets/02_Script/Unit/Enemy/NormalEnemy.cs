using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        HP = 4 * Managers.Instance.Game.FindBaseInitScript<GameTimer>().EnemyHPLevel * 2;
        HPSlider.Slider.maxValue = HP;
        HPSlider.Slider.value = HP;
    }


}
