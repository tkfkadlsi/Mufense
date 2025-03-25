using System.Collections;
using UnityEngine;

public class BlinkEffect : BaseObject
{
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Effect;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        transform.localScale = Vector3.zero;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
        transform.rotation = Quaternion.identity;
        StartCoroutine(EffectCoroutine());
    }

    private IEnumerator EffectCoroutine()
    {
        float t = 0f;
        float lerpTime = 2f;

        Color endColor = Color.clear;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 3f, t / lerpTime);
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, endColor, t / lerpTime);
            transform.Rotate(new Vector3(0, 0, 1), 5f);
        }

        _poolable.PushThisObject();
    }
}
