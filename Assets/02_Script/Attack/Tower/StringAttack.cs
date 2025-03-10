using System.Collections;
using UnityEngine;

public class StringAttack : TowerAttack
{
    private float _speed;
    private Vector3 _direction;

    //private TrailRenderer _trailRenderer;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        //_trailRenderer = GetComponent<TrailRenderer>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        //_trailRenderer.startColor = Managers.Instance.Game.PlayingMusic.PlayerColor;
        //_trailRenderer.endColor = new Color(_trailRenderer.startColor.r, _trailRenderer.startColor.g, _trailRenderer.endColor.b, 0f);
    }

    public override void SettingTarget(Vector3 target, float musicPower)
    {
        base.SettingTarget(target, musicPower);
        _damage = musicPower;
        _speed = Managers.Instance.Game.CurrentBPM / 10f;
        _direction = target;
        transform.up = target;
        StartCoroutine(PushCoroutine());
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private IEnumerator PushCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        //_trailRenderer.enabled = true;
        yield return new WaitForSeconds((Managers.Instance.Game.UnitTime * 4) - 0.2f);
        //_trailRenderer.enabled = false;
        _poolable.PushThisObject();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hit(_damage);
        }
    }

    
}
