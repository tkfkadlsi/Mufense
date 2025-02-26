using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BombTowerAttack : TowerAttack
{
    public override void SettingTarget(Transform target, float musicPower)
    {
        base.SettingTarget(target, musicPower);

        StartCoroutine(BombCoroutine());
    }

    private IEnumerator BombCoroutine()
    { 
        yield return transform.DOJump(_target.position, 1.5f, 1, Managers.Instance.Game.UnitTime * 4f);
        Managers.Instance.Pool.PopObject(PoolType.BombTowerAttackImpact, transform.position).GetComponent<BombTowerAttackImpact>().Setting(_musicPower);
        _poolable.PushThisObject();
    }
}
