using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TowerType
{
    None,
    Piano,
    Drum,
    String,
    CoreRotate,
    CoreRotate2,
    CoreRotate4,
    CoreAttack
}

public abstract class Tower : BaseObject, IMusicPlayHandle, IPointerClickHandler
{
    public int TowerLevel { get; set; }
    public float Damage { get; set; }
    public float Range { get; set; }
    public int Level2Upgrade { get; set; } = 0;
    public int Level3Upgrade { get; set; } = 0;

    [SerializeField] private Sprite _iconSprite;
    protected Enemy _target;
    protected bool _isStun;

    private bool _canInterection;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Tower;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        _canInterection = true;
        _isStun = false;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent += HandleNoteEvent;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().NoteEvent -= HandleNoteEvent;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
        }
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

    public void Stun(float time)
    {
        StartCoroutine(StunCoroutine(time));
    }

    public IEnumerator StunCoroutine(float time)
    {
        _isStun = true;

        StunEffect stunEffect = Managers.Instance.Pool.PopObject(PoolType.StunEffect, transform.position).GetComponent<StunEffect>();
        stunEffect.SettingTime(Vector3.one * 2, time);

        yield return Managers.Instance.Game.GetWaitForSecond(time);
        _isStun = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Managers.Instance.Game.FindBaseInitScript<InGameCamera>().IsLock == true) return;
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("TowerUpCanvas", true);
        Managers.Instance.Game.FindBaseInitScript<InGameCamera>().CamLockToObject(transform);
    }
}
