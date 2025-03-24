using DG.Tweening;
using UnityEngine;

public class BackGround : BaseObject, IMusicPlayHandle
{
    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.BackGround;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return true;
    }

    private void OnDestroy()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.BackGroundColor, 1f);
    }
}
