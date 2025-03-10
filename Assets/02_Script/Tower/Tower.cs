using DG.Tweening;
using UnityEngine;

public enum TowerType
{
    None,
    Piano,
    Drum,
    String,
    Core
}

public abstract class Tower : BaseObject, IMusicPlayHandle
{
    public int TowerLevel { get; set; }
    public float Damage { get; set; }
    public float Range { get; set; }

    [SerializeField] private Sprite _iconSprite;
    protected TowerIcon _towerIcon;
    protected Enemy _target;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Tower;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _towerIcon = Managers.Instance.Pool.PopObject(PoolType.TowerIcon, transform.position).GetComponent<TowerIcon>();
        _towerIcon.TowerIconSetting(_iconSprite, this);
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent += HandleNoteEvent;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent -= HandleNoteEvent;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
        }
        _towerIcon.PushThisObject();
        _towerIcon = null;
        base.Release();
    }

    protected abstract void HandleNoteEvent(TowerType type);

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }
}
