using DG.Tweening;
using UnityEngine;

public abstract class Attack : BaseObject, IMusicPlayHandle
{
    protected Collider2D _collider;

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerAttackColor, 1f);
    }

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }


        _objectType = ObjectType.Attacks;
        _collider = gameObject.GetOrAddComponent<Collider2D>();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _collider.isTrigger = true;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerAttackColor;
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
