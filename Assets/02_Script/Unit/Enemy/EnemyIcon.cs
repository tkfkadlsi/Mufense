using DG.Tweening;

public class EnemyIcon : BaseObject, IMusicPlayHandle
{
    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.EnemyIcon;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.TextColor;

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

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.TextColor, 1f);
    }
}
