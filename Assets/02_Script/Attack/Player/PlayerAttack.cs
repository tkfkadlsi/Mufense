using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PoolableObject))]
public class PlayerAttack : Attack
{
    private PoolableObject _poolable;

    private Vector3 _direction;
    private float _speed;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = gameObject.GetOrAddComponent<PoolableObject>();

        _speed = 20f;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        _direction = mousePos - transform.position;

        StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        transform.position += _direction.normalized * _speed * Time.deltaTime;
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _poolable.PushThisObject();
    }
}