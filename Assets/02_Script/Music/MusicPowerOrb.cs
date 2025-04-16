using DG.Tweening;
using System.Collections;
using UnityEngine;


public class MusicPowerOrb : BaseObject
{
    private TrailRenderer _trailRenderer;
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _trailRenderer = GetComponent<TrailRenderer>();
        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.MusicPowerOrb;

        _trailRenderer.minVertexDistance = 0.005f;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
        _trailRenderer.startColor = Managers.Instance.Game.PlayingMusic.EnemyColor;
        _trailRenderer.endColor = Color.clear;
        _trailRenderer.Clear();

        Vector3 startPos = transform.position;
        Vector3 endPos = Managers.Instance.UI.GameRootUI.MainCanvas.GetPickPos();

        Vector3 peakPos = new Vector3((startPos.x + endPos.x) * 0.5f, startPos.y - 1);


        transform.DOPath(new Vector3[]
        {
            startPos,
            peakPos,
            endPos,

        }, 1.5f, PathType.CatmullRom).SetEase(Ease.InCubic)
            .OnComplete(() =>
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().AddMusicPower(1);
            _poolable.PushThisObject();
        });
    }
}
