using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DrumAttack : TowerAttack
{
    private float _range;

    protected override void Setting()
    {
        base.Setting();
        transform.localScale = Vector3.one;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
    }

    public override void SettingTarget(Vector3 target, float musicPower)
    {
        base.SettingTarget(target, musicPower);
        _damage = musicPower;
        _range = target.x;
        StartCoroutine(DrumCoroutine());
    }

    private IEnumerator DrumCoroutine()
    {
        _spriteRenderer.DOColor(Color.clear, Managers.Instance.Game.UnitTime).SetEase(Ease.Linear);
        transform.DOScale(_range, Managers.Instance.Game.UnitTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(Managers.Instance.Game.UnitTime);
        _poolable.PushThisObject();
    }
}
