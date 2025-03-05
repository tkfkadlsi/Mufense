using DG.Tweening;
using UnityEngine;

public abstract class Attack : BaseObject, IMusicPlayHandle
{
    protected Collider2D _collider;

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }


        _objectType = ObjectType.Attacks;
        _collider = gameObject.GetOrAddComponent<Collider2D>();


        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
        _collider.isTrigger = true;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }
}
