using DG.Tweening;
using System.Collections;
using UnityEngine;



public abstract class Enemy : Unit, IHealth, IMusicPlayHandle
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

    protected bool _isStun;

    private PoolableObject _poolable;

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

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
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


}
