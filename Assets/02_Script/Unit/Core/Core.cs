using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Core : Unit, IMusicPlayHandle, IHealth
{
    public event Action<float> HPChangeEvent;
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }
    public float Damage {  get; private set; }


    private IEnumerator HitCoroutine;
    private float _plusAngle;

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
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent += HandleNoteEvent;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;

            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent -= HandleNoteEvent;
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
        _plusAngle = 360f / music.BeatInBar;
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

    private void HandleNoteEvent(TowerType type)
    {
        if(type == TowerType.CoreRotate)
        {
            CoreRotate(1);
        }
        if(type == TowerType.CoreRotate2)
        {
            CoreRotate(2);
        }
        if(type == TowerType.CoreRotate4)
        {
            CoreRotate(4);
        }
        if(type == TowerType.CoreAttack)
        {
            CoreAttack();
        }
    }

    private void CoreRotate(int multiple)
    {
        float lerpTime = Managers.Instance.Game.UnitTime * multiple;

        float startAngle = transform.localEulerAngles.z;
        float endAngle = transform.localEulerAngles.z + _plusAngle * multiple;

        transform.DOLocalRotate(new Vector3(0, 0, endAngle), lerpTime * 0.9f).SetEase(Ease.InOutExpo);
    }

    private void CoreAttack()
    {
        Managers.Instance.Pool.PopObject(PoolType.CircleArc, transform.position).GetComponent<CircleArcAttack>();
    }
}