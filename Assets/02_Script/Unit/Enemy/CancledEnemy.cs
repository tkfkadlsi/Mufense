public class CancledEnemy : Enemy
{
    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _originSpeed = 2f;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        HP = 1;
        HPSlider.Slider.maxValue = HP;
        HPSlider.Slider.value = HP;
    }

    public override void Hit(float damage, int debuff = 0, Tower attacker = null)
    {
        if (attacker != null)
        {
            attacker.Stun(Managers.Instance.Game.UnitTime * 4);
        }
        base.Hit(damage, debuff, attacker);
    }
}
