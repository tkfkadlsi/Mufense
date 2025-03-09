using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PianoAttack : TowerAttack
{
    private float _speed;
    private Vector3 _direction;

   public override void SettingTarget(Vector3 target, float musicPower)
    {
        base.SettingTarget(target, musicPower);
        _speed = Managers.Instance.Game.CurrentBPM / 7.5f;
        _direction = target;
        StartCoroutine(PushCoroutine());
    }

    private IEnumerator PushCoroutine()
    {
        yield return new WaitForSeconds(Managers.Instance.Game.UnitTime * 2);
        _poolable.PushThisObject();
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hit(_damage);
            _poolable.PushThisObject();
        }
    }
}
