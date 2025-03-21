using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;   

public abstract class Treasure : BaseObject, IMusicPlayHandle
{
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Sprite _openedSprite;

    protected PoolableObject _poolable;
    protected bool _canInterection;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Treasure;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _canInterection = true;
        _spriteRenderer.sprite = _closedSprite;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        Managers.Instance.Pool.PopObject(PoolType.TreasureSpawnEffect, transform.position);
        SetPosition();
    }

    protected override void Release()
    {
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
        pos = pos.normalized * Random.Range(15f, 30f);
        transform.position = pos;
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }
}
