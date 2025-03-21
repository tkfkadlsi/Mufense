using DG.Tweening;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TreasureSpawnEffect : BaseObject, IMusicPlayHandle
{
    private PoolableObject _poolable;
    private List<Collider2D> _enemies = new List<Collider2D>();

    protected override bool Init()
    {
        if(base.Init() == false)
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
        _enemies.Clear();
        StartCoroutine(EffectCoroutine());
    }

    private IEnumerator EffectCoroutine()
    {
        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime * 4f;

        while( t < lerpTime )
        {
            t += Time.deltaTime;
            yield return null;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 25f, t / lerpTime);
        }

        _poolable.PushThisObject();
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_enemies.Contains(collision)) return;
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            _enemies.Add(collision);

            Vector2 dir = enemy.transform.position - transform.position;
            dir = dir.normalized * ( 1f / (dir.magnitude * dir.magnitude));

            enemy.Knockback(dir);
        }
    }
}
