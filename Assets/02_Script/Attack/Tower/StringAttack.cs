using System.Collections;
using UnityEngine;

public class StringAttack : TowerAttack
{
    private float _speed;
    private Vector3 _direction;

    private TrailRenderer[] _trailRenderers;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _trailRenderers = GetComponentsInChildren<TrailRenderer>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        Color color = Managers.Instance.Game.PlayingMusic.PlayerColor;

        for(int i = 0; i <  _trailRenderers.Length; i++)
        {
            color.a = 0.8f;
            _trailRenderers[i].startColor = color;
            color.a = 0f;
            _trailRenderers[i].endColor = color;
            _trailRenderers[i].Clear();
        }
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
        yield return new WaitForSeconds((Managers.Instance.Game.UnitTime * 2));
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
