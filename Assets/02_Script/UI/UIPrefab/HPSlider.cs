using UnityEngine;
using UnityEngine.UI;

public class HPSlider : BaseUI
{
    private PoolableObject _poolable;

    private enum ESliders
    {
        Slider
    }

    public Slider Slider;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Slider>(typeof(ESliders));
        Slider = Get<Slider>((int)ESliders.Slider);

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
