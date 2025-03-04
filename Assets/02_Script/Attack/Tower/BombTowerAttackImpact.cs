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
        transform.DOScale(7.5f, 0.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(0f, 0.5f).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(0.5f);
        _poolable.PushThisObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().HP -= _musicPower;
        }
    }
}
