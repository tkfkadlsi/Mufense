using DG.Tweening;
using UnityEngine;

public class StunEffect : BaseObject
{
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.FrontEffect;
        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public void SettingTime(Vector3 scale, float time)
    {
        transform.localScale = scale;

        transform.DOScale(Vector3.zero, time).SetEase(Ease.InExpo).OnComplete(() =>
        {
            _poolable.PushThisObject();
        });
    }
}
