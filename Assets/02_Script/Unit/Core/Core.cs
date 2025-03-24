using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Core : Unit, IMusicPlayHandle, IHealth
{
    public event Action<float> HPChangeEvent;
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

    private float _damage;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Core;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _damage = 1f;
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
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent -= HandleNoteEvent;
        }

        base.Release();
    }

    private void Update()
    {
        transform.Rotate(0, 0, (120f / Managers.Instance.Game.UnitTime) * Time.deltaTime);
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
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    public void HandleNoteEvent(TowerType type)
    {
        if (type == TowerType.Core)
        {
            Vector3 randPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            CoreAttack coreAttack = Managers.Instance.Pool.PopObject(PoolType.CoreAttack, transform.position).GetComponent<CoreAttack>();
            coreAttack.Attack(randPos, _damage);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Hit(float damage, int debuff = 0, Tower attacker = null)
    {
        HP -= damage;

        if(HP <= 0f)
        {
            Die();
        }
        HPSlider.Slider.value = HP;
        HPChangeEvent?.Invoke(HP);
    }

    public void Heal(float heal)
    {
        HP += heal;
        if(HP > 100f)
        {
            HP = 100f;
        }
        HPSlider.Slider.value = HP;
        HPChangeEvent?.Invoke(HP);
    }

    public void Die()
    {
        //게임오버
    }
}