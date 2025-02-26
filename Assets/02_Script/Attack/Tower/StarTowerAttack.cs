using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StarTowerAttack : TowerAttack
{
    private readonly float _speed = 10f;

    private bool _tracking = true;

    public override void SettingTarget(Transform target, float musicPower)
    {
        base.SettingTarget(target, musicPower);

        _tracking = true;
        transform.localScale = Vector3.one;
        _spriteRenderer.color = Color.white;

        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }

    private void Update()
    {
        if (_tracking == false) return;

        if (_target == null)
        {
            _poolable.PushThisObject();
        }

        Vector3 direction = _target.transform.position - transform.position;
        transform.position += direction.normalized * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_tracking == true && collision.transform == _target)
        {
            _tracking = false;
            _target.GetComponent<Enemy>().HP -= _musicPower;
            StartCoroutine(ImpactCoroutine());
        }

        if(_tracking == false && collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.HP -= _musicPower;
        }
    }

    private IEnumerator ImpactCoroutine()
    {
        transform.DOScale(5f, 0.25f).SetEase(Ease.OutCirc);
        yield return _spriteRenderer.DOColor(Color.clear, 0.5f).SetEase(Ease.OutCirc);
        _poolable.PushThisObject();
    }
}
