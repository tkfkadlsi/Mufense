using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BombTowerAttackImpact : BaseInit
{
    private PoolableObject _poolable;
    private float _musicPower;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    public void Setting(float musicPower)
    {
        _musicPower = musicPower;
        transform.localScale = Vector3.zero;
        StartCoroutine(BombCoroutine());
    }

    private IEnumerator BombCoroutine()
    {
        yield return transform.DOScale(7.5f, 0.5f).SetEase(Ease.OutExpo);
        yield return transform.DOScale(0f, 0.5f).SetEase(Ease.InExpo);
        _poolable.PushThisObject();
    }
}
