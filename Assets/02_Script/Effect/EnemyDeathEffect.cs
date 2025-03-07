using System.Collections;
using UnityEngine;

public class EnemyDeathEffect : BaseObject
{
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _objectType = ObjectType.Effect;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
        transform.localScale = Vector3.one * 0.1f;

        StartCoroutine(EffectCoroutine());
    }

    private IEnumerator EffectCoroutine()
    {
        float t = 0f;
        float lerpTime = 1.5f;

        Color endColor = Color.clear;

        while (t <= lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.5f, t / lerpTime);
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, endColor, t / lerpTime);
        }

        _poolable.PushThisObject();
    }
}
