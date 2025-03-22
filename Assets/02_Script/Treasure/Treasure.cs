using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;   

public abstract class Treasure : BaseObject, IMusicPlayHandle
{
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Sprite _openedSprite;

    protected PoolableObject _poolable;
    protected bool _canInterection;

    private bool _isSetting;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Treasure;
        _isSetting = false;


        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        if(_isSetting == false)
        {
            _isSetting = true;
            return;
        }

        _canInterection = true;
        _spriteRenderer.sprite = _closedSprite;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        SetPosition();
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
        Managers.Instance.Pool.PopObject(PoolType.TreasureSpawnEffect, transform.position);
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        _canInterection = false;
        base.Release();
    }

    protected abstract void Reward();

    public void Interection()
    {
        if (_canInterection == false) return;
        _canInterection = false;

        _spriteRenderer.sprite = _openedSprite;
        Reward();
    }

    private void SetPosition()
    {
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        pos = pos.normalized * Random.Range(15f, 25f);
        transform.position = pos;
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }
}
