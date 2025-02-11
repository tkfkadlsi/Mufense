using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Core : Unit, IMusicPlayHandle
{
    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Core;

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return true;
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
        transform.Rotate(0, 0, (77f / Managers.Instance.Game.UnitTime) * Time.deltaTime);
    }

    protected override void Setting()
    {
        base.Setting();
    }

    public void CircleArcAttack()
    {
        Managers.Instance.Pool.PopObject(PoolType.CircleArc, transform.position);
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.CoreColor, 1f);
    }
}