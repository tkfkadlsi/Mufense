using UnityEngine;

public enum TowerType
{
    None,
    Piano,
    Drum,
    String,
    Core
}

public abstract class Tower : BaseObject
{
    [SerializeField] private Sprite _iconSprite;
    protected TowerIcon _towerIcon;
    private Enemy _target;

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
        _towerIcon.TowerIconSetting(_iconSprite);
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent += HandleNoteEvent;
    }

    protected override void Release()
    {
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent -= HandleNoteEvent;
        _towerIcon.PushThisObject();
        _towerIcon = null;
        base.Release();
    }

    protected abstract void HandleNoteEvent(TowerType type);
}
