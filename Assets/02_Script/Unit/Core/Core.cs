using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Core : Unit, IMusicPlayHandle, IHealth
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }

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

        HPSlider = Managers.Instance.Pool.PopObject(PoolType.HPSlider, transform.position + Vector3.up * 2f).GetComponent<HPSlider>();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
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
        transform.Rotate(0, 0, (120f / Managers.Instance.Game.UnitTime) * Time.deltaTime);
    }

    public void CircleArcAttack()
    {
        Managers.Instance.Pool.PopObject(PoolType.CircleArc, transform.position);
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    public void Hit(float damage, int debuff = 0)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}