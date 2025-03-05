using UnityEngine;

public enum TowerType
{
    None,
    Piano,
    Drum,
    String,
}

public class Tower : BaseObject
{
    private TowerType _type;
    private TowerIcon _icon;
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

        Managers.Instance.Pool.PopObject(PoolType.TowerIcon, transform.position);
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent += HandleNoteEvent;
    }

    protected override void Release()
    {
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent -= HandleNoteEvent;
        _icon.PushThisObject();
        base.Release();
    }

    public void TowerSetting(TowerType type, Sprite sprite)
    {
        _type = type;
        _icon.TowerIconSetting(sprite);
    }

    private void HandleNoteEvent(TowerType type)
    {
        if (type != _type) return;

        switch(type)
        {
            case TowerType.Piano:

                break;
            case TowerType.Drum:

                break;
            case TowerType.String:

                break;
        }
    }
}
