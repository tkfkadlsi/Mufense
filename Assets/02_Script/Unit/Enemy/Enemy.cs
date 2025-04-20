using DG.Tweening;
using System.Collections;
using UnityEngine;



public abstract class Enemy : Unit, IHealth, IMusicPlayHandle, IMusicHandleObject
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

    protected bool _isStun;
    protected int _moveCooltime;

    private PoolableObject _poolable;
    private Way _currentWay;

    private int _moveCooldown;

    private IEnumerator MoveCoroutine;
    private IEnumerator HitedCoroutine;
    private IEnumerator StunCoroutine;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Enemy;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        HPSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position).GetComponent<HPSlider>();
        _collider.isTrigger = true;

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    public void EnemySetting(Way way)
    {
        base.Setting();

        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
        transform.rotation = Quaternion.identity;
        _isStun = false;
        _currentWay = way;
        _moveCooldown = _moveCooltime;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        EnemySpawner.SavedEnemyCount--;
        HPSlider.PushThisObject();
        HPSlider = null;
        base.Release();
    }

    private void Update()
    {
        HPSlider.transform.position = transform.position + Vector3.up * 0.5f;
    }


    public virtual void Hit(float damage, int debuff = 0, Tower attacker = null)
    {
        HP -= damage;

        if (HP <= 0f)
        {
            Die();
            return;
        }
        HPSlider.Slider.value = HP;

        if (HitedCoroutine is not null)
        {
            StopCoroutine(HitedCoroutine);
        }
        HitedCoroutine = Hited(0.5f);
        StartCoroutine(HitedCoroutine);

        if ((debuff & (int)Debuffs.Stun) == (int)Debuffs.Stun)
        {
            if (StunCoroutine is not null)
            {
                StopCoroutine(StunCoroutine);
            }
            StunCoroutine = Stun(3f);
            StartCoroutine(StunCoroutine);
        }
    }

    public IEnumerator Hited(float lerpTime)
    {
        float t = 0f;

        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Managers.Instance.Game.PlayingMusic.EnemyColor, t / lerpTime);
        }
    }

    private IEnumerator Stun(float time)
    {

        StunEffect stunEffect = Managers.Instance.Pool.PopObject(PoolType.StunEffect, transform.position).GetComponent<StunEffect>();
        stunEffect.SettingTime(Vector3.one, time);
        _isStun = true;

        yield return Managers.Instance.Game.GetWaitForSecond(time);

        _isStun = false;
    }


    public void Die()
    {
        Managers.Instance.Pool.PopObject(PoolType.EnemyDeathEffect, transform.position);
        Managers.Instance.Pool.PopObject(PoolType.MusicPowerOrb, transform.position);
        _poolable.PushThisObject();
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Core"))
        {
            Managers.Instance.Game.FindBaseInitScript<Core>().Hit(HP);
            PushThisObject();
        }
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.EnemyColor, 1f);
    }

    public void HandleMusicBeat()
    {
        _moveCooldown++;
        if(_moveCooldown > _moveCooltime)
        {
            _moveCooldown -= _moveCooltime;

            _currentWay = _currentWay.GetNextWay();
            if (MoveCoroutine is not null)
            {
                StopCoroutine(MoveCoroutine);
            }
            MoveCoroutine = Move();
            StartCoroutine(MoveCoroutine);
        }
    }

    private IEnumerator Move()
    {
        if (_currentWay == null) yield break;

        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime;
        Vector3 endPos = _currentWay.transform.position;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            transform.position = Vector3.Lerp(transform.position, endPos, t / lerpTime);
        }
    }

    protected void Jump(int count)
    {
        for(int i = 0; i < count; i++)
        {
            if(_currentWay.GetNextWay() != null)
            {
                _currentWay = _currentWay.GetNextWay();
            }
        }

        if(MoveCoroutine is not null)
        {
            StopCoroutine(MoveCoroutine);
            MoveCoroutine = null;
        }

        transform.position = _currentWay.transform.position;
    }
}
