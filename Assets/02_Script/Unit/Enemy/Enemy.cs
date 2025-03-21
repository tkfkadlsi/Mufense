using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;



public class Enemy : Unit, IHealth, IMusicPlayHandle
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

    private PoolableObject _poolable;

    protected float _originSpeed = 1.25f;
    private float _speed;

    protected Core _core;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _core = Managers.Instance.Game.FindBaseInitScript<Core>();
        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Enemy;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        HPSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position).GetComponent<HPSlider>();
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;

        _speed = _originSpeed;
        HP = 6 + Managers.Instance.Game.FindBaseInitScript<GameTimer>().EnemyHPLevel * 2;
        HPSlider.Slider.maxValue = HP;
        HPSlider.Slider.value = HP;

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        HPSlider.PushThisObject();
        HPSlider = null;
        base.Release();
    }

    private void Update()
    {
        Vector3 direction = (_core.transform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
        transform.up = direction;

        if(HPSlider != null)
        {
            HPSlider.transform.position = transform.position + Vector3.up;
        }
    }

    private IEnumerator HitedCoroutine;
    private IEnumerator StunCoroutine;
    private IEnumerator PoisonCoroutine;

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
            if(StunCoroutine is not null)
            {
                StopCoroutine(StunCoroutine);
            }
            StunCoroutine = Stun(3f);
            StartCoroutine(StunCoroutine);
        }
        if ((debuff & (int)Debuffs.Poison) == (int)Debuffs.Poison)
        {
            if (PoisonCoroutine is not null)
            {
                StopCoroutine(PoisonCoroutine);
            }
            PoisonCoroutine = Poison(damage);
            StartCoroutine(PoisonCoroutine);
        }
    }

    public IEnumerator Hited(float lerpTime)
    {
        float t = 0f;

        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Managers.Instance.Game.PlayingMusic.EnemyColor, t / lerpTime);
        }
    }

    private IEnumerator Stun(float time)
    {
        _speed = 0f;

        StunEffect stunEffect = Managers.Instance.Pool.PopObject(PoolType.StunEffect, transform.position).GetComponent<StunEffect>();
        stunEffect.SettingTime(Vector3.one, time);

        yield return new WaitForSeconds(time);
        _speed = _originSpeed;
    }

    private IEnumerator Poison(float damage)
    {
        float t = 0f;
        float lerpTime = 1f;
        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            HP -= damage * Time.deltaTime;
            HPSlider.Slider.value = HP;
        }
    }

    public void Knockback(Vector2 vector)
    {
        _rigidbody.AddForce(vector, ForceMode2D.Impulse);
    }

    public void Die()
    {
        Managers.Instance.Pool.PopObject(PoolType.EnemyDeathEffect, transform.position);
        Managers.Instance.Pool.PopObject(PoolType.MusicPowerOrb, transform.position);
        _poolable.PushThisObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Core"))
        {
            collision.collider.GetComponent<Core>().Hit(HP);
            Die();
        }
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.EnemyColor, 1f);
    }
}
