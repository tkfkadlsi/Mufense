using UnityEngine;

public class NormalTowerAttack : TowerAttack
{
    private readonly float _speed = 5f;

    private void Update()
    {
        if(_target == null)
        {
            _poolable.PushThisObject();
        }

        Vector3 direction = _target.transform.position - transform.position;
        transform.position += direction.normalized * _speed * Time.deltaTime;
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
