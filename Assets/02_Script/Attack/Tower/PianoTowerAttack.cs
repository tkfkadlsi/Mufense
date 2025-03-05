using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PianoTowerAttack : TowerAttack
{
    public override void SettingTarget(Enemy target, float musicPower)
    {
        base.SettingTarget(target, musicPower);
        StartCoroutine(PianoAttack());
    }

    private IEnumerator PianoAttack()
    {
        float t = 0f;
        float lerpTime = Managers.Instance.Game.UnitTime;

        Vector3 startPos = transform.position;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            if(_target == null)
            {
                _poolable.PushThisObject();
            }

            transform.position = Vector3.Lerp(startPos, _target.transform.position, t / lerpTime);
        }

        _target.Hit(_damage);
    }
}
