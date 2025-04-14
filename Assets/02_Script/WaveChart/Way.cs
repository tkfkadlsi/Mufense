using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class Way : BaseObject, IMusicPlayHandle
{
    [SerializeField] private Way _nextWay;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Way;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

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

    public Way GetNextWay()
    {
        return _nextWay;
    }

    public void SetNextWay(Way nextWay)
    {
        if (_nextWay != null) return;
        _nextWay = nextWay;
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.TextColor, 1f);
    }
}
