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

    private Slider _slider;

    private Image _backgroundImage;
    private Image _fillImage;

    private Canvas _canvas;

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

        _slider = Get<Slider>((int)ESliders.Slider);

        _backgroundImage = Get<Image>((int)EImages.Background);
        _fillImage = Get<Image>((int)EImages.Fill);

        _originBackgroundColor = _backgroundImage.color;
        _originFillColor = _fillImage.color;

        _poolable = GetComponent<PoolableObject>();
        _canvas = GetComponent<Canvas>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _backgroundImage.color = _originBackgroundColor;
        _fillImage.color = _originFillColor;
        _canvas.enabled = false;
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

        yield return Managers.Instance.Game.GetWaitForSecond(time);

        _backgroundImage.color = _originBackgroundColor;
        _fillImage.color = _originFillColor;
    }

    public void SetMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
        _canvas.enabled = true;
    }
}
