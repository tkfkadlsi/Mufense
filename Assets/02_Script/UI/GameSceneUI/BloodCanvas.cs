using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BloodCanvas : BaseUI
{
    private float _alpha;

    enum EImages
    {
        BloodImage
    }

    private Image _bloodImage;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Image>(typeof(EImages));

        _bloodImage = Get<Image>((int)EImages.BloodImage);
        _bloodImage.color = Color.clear;
        _alpha = 0f;

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        Managers.Instance.Game.FindBaseInitScript<Core>().HPChangeEvent += SettingAlpha;
    }

    protected override void ActiveOff()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<Core>().HPChangeEvent -= SettingAlpha;
        }

        base.ActiveOff();
    }

    private void SettingAlpha(float hp)
    {
        if (hp > 75f)
        {
            _bloodImage.color = Color.clear;
            return;
        }
        _alpha = 1 - (hp / 100f);
        Color color = Color.white;
        color.a = _alpha;
        _bloodImage.color = color;
    }
}
