using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum EnemyState
{
    Create,
    Active,
    Death
}

public class Enemy : Unit, IHealth, IMusicPlayHandle
{
    private EnemyState _state;
    private float _speed;
    private float _originSpeed;
    private int _musicPower;

    private Player _player;
    private Core _core;

    private Transform _target;
    private PoolableObject _poolable;
    //private HPSlider _hpSlider;

    private IEnumerator HitedCoroutine;

    private float _hp;
    public float HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if(_hp <= 0f)
            {
                Die(true);
            }
            else
            {
                if(HitedCoroutine is not null)
                {
                    StopCoroutine(HitedCoroutine);
                }
                HitedCoroutine = Hited();
                StartCoroutine(HitedCoroutine);
                //_hpSlider.Slider.value = _hp;
            }
        }
    }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Enemy;
        _poolable = GetComponent<PoolableObject>();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return true;
    }

    private void OnDisable()
    {
        //if(_hpSlider != null)
        //{
        //    _hpSlider.PushThisObject();
        //    _hpSlider = null;
        //}
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }

    private void Update()
    {
        if (_state != EnemyState.Active) return;
        if(4f >= Vector3.Distance(_player.transform.position, transform.position))
        {
            _target = _player.transform;
        }
        else
        {
            _target = _core.transform;
        }

        Vector3 direction = (_target.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
        //_hpSlider.transform.position = transform.position + Vector3.up;
    }

    protected override void Setting()
    {
        base.Setting();
        _state = EnemyState.Create;
        _player = FindAnyObjectByType<Player>();
        _core = FindAnyObjectByType<Core>();
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
    }

    public void EnemySetting(float hp, float speed, Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _hp = hp;
        _musicPower = (int)hp;
        _speed = speed;
        _originSpeed = speed;
        _state = EnemyState.Active;
        //_hpSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position + Vector3.up).GetComponent<HPSlider>();
        //_hpSlider.Slider.maxValue = hp;
        //_hpSlider.Slider.value = hp;
    }

    public void Die(bool drop)
    {
        _state = EnemyState.Death;
;

        if(drop == true)
        {
            while (_musicPower >= 5)
            {
                SpawnOrb(5);
            }


            while (_musicPower > 0)
            {
                SpawnOrb(1);
            }
        }


        _poolable.PushThisObject();
    }

    private void SpawnOrb(int musicPower)
    {
        MusicPowerOrb musicPowerOrb;
        _musicPower -= musicPower;
        musicPowerOrb = Managers.Instance.Pool.PopObject(PoolType.MusicPowerOrb, transform.position).GetComponent<MusicPowerOrb>();
        musicPowerOrb.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Managers.Instance.Pool.PopObject(PoolType.EnemyDeathEffect, transform.position);
        musicPowerOrb.SetMusicPower(musicPower);
    }

    public IEnumerator Hited()
    {
        float t = 0f;
        float lerpTime = 1f;

        while (t < lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            _speed = Mathf.Lerp(0f, _originSpeed, t / lerpTime);
        }
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.EnemyColor, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Core"))
        {
            collision.collider.GetComponent<Core>().HP -= _musicPower;
            Managers.Instance.Pool.PopObject(PoolType.EnemyDeathEffect, transform.position);
            Die(false);
        }
    }
}
