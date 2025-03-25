using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TreasureSpawnEffect : BaseObject, IMusicPlayHandle
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
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        StartCoroutine(EffectCoroutine());
    }

    private IEnumerator EffectCoroutine()
    {
        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime * 4f;

        Color startColor = Managers.Instance.Game.PlayingMusic.PlayerColor;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 30f, t / lerpTime);
            _spriteRenderer.color = Color.Lerp(startColor, Color.clear, t / lerpTime);
        }

        _poolable.PushThisObject();
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }
}
