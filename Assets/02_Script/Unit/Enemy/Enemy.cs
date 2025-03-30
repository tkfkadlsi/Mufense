using DG.Tweening;
using System.Collections;
using UnityEngine;



public abstract class Enemy : Unit, IHealth, IMusicPlayHandle, IMusicHandleObject
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

    protected bool _isStun;
    protected int _moveAmount;
    protected int _moveCooltime;
    protected int _moveCooldown;
    protected Vector3 _targetPosition { get; private set; }

    private PoolableObject _poolable;
    private int _moveIndex;
    private int _wayNumber;

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

    public void EnemySetting(int wayNumber)
    {
        base.Setting();
        HPSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position).GetComponent<HPSlider>();
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
        transform.rotation = Quaternion.identity;
        _isStun = false;
        _moveCooldown = 0;

        _wayNumber = wayNumber;
        _moveIndex = 0;

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;
        }

        HPSlider.PushThisObject();
        HPSlider = null;
        base.Release();
    }

    private IEnumerator MoveCoroutine;
    private IEnumerator HitedCoroutine;
    private IEnumerator StunCoroutine;

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

    private IEnumerator Move()
    {
        float t = 0f;
        float lerptime = Managers.Instance.Game.UnitTime;

        while (t < lerptime)
        {
            t += Time.deltaTime;
            yield return null;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, t / lerptime);
        }
    }

    public void Die()
    {
        Managers.Instance.Pool.PopObject(PoolType.EnemyDeathEffect, transform.position);
        Managers.Instance.Pool.PopObject(PoolType.MusicPowerOrb, transform.position);
        Managers.Instance.Game.FindBaseInitScript<WaveController>().CurrentWave.HandleDeathEnemy(this);
        _poolable.PushThisObject();
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Core"))
        {
            collision.collider.GetComponent<Core>().Hit(HP);
            Die();
        }
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.EnemyColor, 1f);
    }

    public void HandleMusicBeat()
    {
        _moveCooldown++;
        if (_moveCooldown > _moveCooltime)
        {
            _moveCooldown -= _moveCooltime;
            _moveIndex += _moveAmount;
            SetPosition();

            if (MoveCoroutine is not null)
            {
                StopCoroutine(MoveCoroutine);
            }
            MoveCoroutine = Move();
            StartCoroutine(MoveCoroutine);
        }
    }

    protected void SetPosition()
    {
        _targetPosition = Managers.Instance.Game.FindBaseInitScript<WaveController>()
                            .CurrentWave.WayObject.GetTargetPosition(_wayNumber, _moveIndex);
    }

    protected void BackBlink(int value)
    {
        _moveIndex -= value;
        if(_moveIndex < 0 )
            _moveIndex = 0;

        SetPosition();

        transform.position = _targetPosition;
    }
}
