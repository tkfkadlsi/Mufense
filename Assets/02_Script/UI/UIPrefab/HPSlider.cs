using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : BaseUI
{
    private PoolableObject _poolable;

    private enum ESliders
    {
        Slider
    }

    private enum EImages
    {
        Background,
        Fill
    }

    public Slider Slider;

    private Image _backgroundImage;
    private Image _fillImage;

    private Color _originBackgroundColor;
    private Color _originFillColor;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Slider>(typeof(ESliders));
        Bind<Image>(typeof(EImages));

        Slider = Get<Slider>((int)ESliders.Slider);

        _backgroundImage = Get<Image>((int)EImages.Background);
        _fillImage = Get<Image>((int)EImages.Fill);

        _originBackgroundColor = _backgroundImage.color;
        _originFillColor = _fillImage.color;

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _backgroundImage.color = _originBackgroundColor;
        _fillImage.color = _originFillColor;
    }

    public void PushThisObject()
    {
        _backgroundImage.color = _originBackgroundColor;
        _fillImage.color = _originFillColor;
        _poolable.PushThisObject();
    }

    public void ChangeColor(Color color, float time)
    {
        StartCoroutine(ChangeColorCoroutine(color, time));
    }

    private IEnumerator ChangeColorCoroutine(Color color, float time)
    {
        _backgroundImage.color = color * 0.5f;
        _fillImage.color = color;

        yield return new WaitForSeconds(time);

        _backgroundImage.color = _originBackgroundColor;
        _fillImage.color = _originFillColor;
    }
}
