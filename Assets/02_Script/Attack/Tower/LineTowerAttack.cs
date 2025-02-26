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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == _target)
        {
            _target.GetComponent<Enemy>().HP -= _musicPower;
            _poolable.PushThisObject();
        }
    }
}
