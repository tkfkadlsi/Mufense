using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;



public class Enemy : Unit, IHealth
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

    private PoolableObject _poolable;

    private readonly float _originSpeed = 5f;
    private float _speed = 5f;

    private Core _core;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _core = Managers.Instance.Game.FindBaseInitScript<Core>();
        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _speed = _originSpeed;
        HPSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position).GetComponent<HPSlider>();
        HP = 5 + Managers.Instance.Game.FindBaseInitScript<GameTimer>().EnemyHPLevel * 3;
        HPSlider.Slider.maxValue = HP;
        HPSlider.Slider.value = HP;
    }

    protected override void Release()
    {
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

    public void Hit(float damage, int debuff = 0)
    {
        HP -= damage;
        HPSlider.Slider.value = HP;

        if (HP <= 0f)
        {
            Die();
            return;
        }

        if (HitedCoroutine is not null)
        {
            StopCoroutine(HitedCoroutine);
        }
        HitedCoroutine = Hited();
        StartCoroutine(HitedCoroutine);

        if ((debuff & (int)Debuffs.Stun) == (int)Debuffs.Stun)
        {
            if(StunCoroutine is not null)
            {
                StopCoroutine(StunCoroutine);
            }
            StunCoroutine = Stun();
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

    private IEnumerator Hited()
    {
        float t = 0f;
        float lerpTime = 0.5f;

        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Managers.Instance.Game.PlayingMusic.EnemyColor, t / lerpTime);
        }
    }

    private IEnumerator Stun()
    {
        _speed = 0f;
        HPSlider.ChangeColor(Color.yellow, 0.5f);
        yield return new WaitForSeconds(0.5f);
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

    public void Die()
    {
        Managers.Instance.Pool.PopObject(PoolType.EnemyDeathEffect, transform.position);
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
}
