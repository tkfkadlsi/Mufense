using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class CoreAttack : Attack
{
    private PoolableObject _poolable;

    private float _damage;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _collider.enabled = false;

        return true;
    }

    public void Attack(Vector3 target, float damage)
    {
        _damage = damage;

        Vector3 direction = target - transform.position;
        transform.up = direction.normalized;
        transform.position += transform.up * 25f;
        transform.localScale = new Vector3(0.2f, 50f, 1f);

        StartCoroutine(PushCoroutine());
    }

    protected override void Release()
    {
        transform.localScale = Vector3.one;

        base.Release();
    }

    private IEnumerator PushCoroutine()
    {
        _collider.enabled = true;

        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime;

        Color startColor = _spriteRenderer.color;
        Color endColor = startColor;
        endColor.a = 0f;

        while(t < lerpTime)
        {
            lerpTime = Managers.Instance.Game.UnitTime;

            t += Time.deltaTime;
            yield return null;

            _spriteRenderer.color = Color.Lerp(startColor, endColor, t / lerpTime);
        }
        _collider.enabled = false;

        _poolable.PushThisObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hit(_damage);
        }
    }
}
