using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Core : Unit, IMusicPlayHandle, IHealth, IMusicHandleObject
{
    public event Action<float> HPChangeEvent;
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }
    public float Damage {  get; private set; }


    private IEnumerator HitCoroutine;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Core;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Damage = 1f;
        HP = 100f;
        HPSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position + Vector3.up * 1.5f).GetComponent<HPSlider>();
        HPSlider.Slider.maxValue = HP;
        HPSlider.Slider.value = HP;
        HPSlider.transform.localScale = new Vector3(0.02f, 0.01f, 0.01f);

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

        base.Release();
    }

    public void CircleArcAttack()
    {
        Managers.Instance.Pool.PopObject(PoolType.CircleArc, transform.position);
    }

    public void StunArcAttack()
    {
        Managers.Instance.Pool.PopObject(PoolType.StunArc, transform.position);
    }

    public void SettingColor(Music music)
    {
        transform.rotation = Quaternion.identity;
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }

    public void Hit(float damage, int debuff = 0, Tower attacker = null)
    {
        HP -= damage;

        if (HP <= 0f)
        {
            Die();
        }
        HPSlider.Slider.value = HP;
        HPChangeEvent?.Invoke(HP);

        if (HitCoroutine is not null)
        {
            StopCoroutine(HitCoroutine);
        }
        HitCoroutine = Hited();
        StartCoroutine(HitCoroutine);
    }

    public void Heal(float heal)
    {
        HP += heal;
        if (HP > 100f)
        {
            HP = 100f;
        }
        HPSlider.Slider.value = HP;
        HPChangeEvent?.Invoke(HP);
    }

    private IEnumerator Hited()
    {
        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _spriteRenderer.color = Color.Lerp(Managers.Instance.Game.PlayingMusic.EnemyColor, Managers.Instance.Game.PlayingMusic.PlayerColor, t / lerpTime);
        }
    }

    public void Die()
    {
        //게임오버

        SceneManager.LoadScene("ResultScene");
    }

    public void HandleMusicBeat()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        Vector3 endrot = (transform.up + transform.right).normalized;

        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            transform.up = Vector3.Lerp(transform.up, endrot, t / lerpTime).normalized;
        }
        transform.up = endrot;

        if(transform.up == Vector3.up)
        {
            Managers.Instance.Pool.PopObject(PoolType.CircleArc, transform.position);
        }
    }
}