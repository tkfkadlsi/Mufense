using UnityEngine;
using System.Collections;

public class StunArcAttack : Attack
{
    PoolableObject _poolable;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        transform.localScale = Vector3.zero;
        StartCoroutine(SlowArcCoroutine());
    }

    private IEnumerator SlowArcCoroutine()
    {
        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime * 4f;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _spriteRenderer.color = Color.Lerp(Color.yellow, Color.clear, t / lerpTime);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 20f, t / lerpTime);
        }

        _poolable.PushThisObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Stun(3f);
        }
    }
}
