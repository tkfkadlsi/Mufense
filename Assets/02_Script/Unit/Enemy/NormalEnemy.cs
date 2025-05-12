using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _moveCooltime = 1;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        HP = 22 + HPLevel * 9;
        HPSlider.Slider.maxValue = HP;
        HPSlider.Slider.value = HP;
    }
}
