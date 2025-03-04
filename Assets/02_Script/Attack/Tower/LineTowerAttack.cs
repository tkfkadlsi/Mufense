using System.Collections;
using UnityEngine;

public class LineTowerAttack : TowerAttack
{ 
    private void OnDisable()
    {
        _collider.enabled = false;
    }

    public override void SettingTarget(Transform target, float musicPower)
    {
        base.SettingTarget(target, musicPower);

        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        _collider.enabled = true;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        _poolable.PushThisObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _target.GetComponent<Enemy>().HP -= _musicPower;
        }
    }
}
