using DG.Tweening;
using System.Collections;
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
    protected bool _isStun;

    private bool _canInterection;

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
        _canInterection = true;
        _isStun = false;
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
        _canInterection = false;
        base.Release();
    }

    protected abstract void HandleNoteEvent(TowerType type);

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    public void Interection()
    {
        if (_canInterection == false) return;
        _canInterection = false;
    }

    public IEnumerator Stun(float time)
    {
        _isStun = true;

        StunEffect stunEffect = Managers.Instance.Pool.PopObject(PoolType.StunEffect, transform.position).GetComponent<StunEffect>();
        stunEffect.SettingTime(Vector3.one * 2, time);

        yield return new WaitForSeconds(time);
        _isStun = false;
    }
}
