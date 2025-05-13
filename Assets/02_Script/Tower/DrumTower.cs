using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DrumTower : Tower
{
    private IEnumerator _shakeCoroutine;

    protected override void Setting()
    {
        base.Setting();

        TowerLevel = 1;
        Damage = 1;
        Range = 4;
    }

    protected override void HandleNoteEvent(TowerType type)
    {
        if (_isStun) return;
        if (type != TowerType.Drum) return;

        if(_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
        }

        _shakeCoroutine = ShakeObject(10f, Managers.Instance.Game.CurrentBPM * 0.1f, Managers.Instance.Game.UnitTime * 0.25f);
        StartCoroutine(_shakeCoroutine);
        
        DrumAttack drumAttack = Managers.Instance.Pool.PopObject(PoolType.DrumAttack, transform.position).GetComponent<DrumAttack>();
        drumAttack.SettingTarget(new Vector3(Range, 0f, 0f), Damage, this);
    }

    private IEnumerator ShakeObject(float amount, float speed, float duration)
    {
        float t = 0f;
        float dummy = 0f;

        while (t < duration)
        {
            dummy += Time.deltaTime * speed;
            float angle = Mathf.Sin(dummy) * amount;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            t += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.identity;
        _shakeCoroutine = null;
    }
}
