using UnityEngine;

public class Enemy : Unit, IHealth
{
    private float _speed;

    private Player _player;
    private Core _core;

    private Transform _target;
    private PoolableObject _poolable;

    private float _hp;
    public float HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if(_hp <= 0f)
            {
                Die();
            }
        }
    }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Enemy;
        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    private void Update()
    {
        if(10f >= Vector3.Distance(_player.transform.position, transform.position))
        {
            _target = _player.transform;
        }
        else
        {
            _target = _core.transform;
        }

        Vector3 direction = (_target.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    protected override void Setting()
    {
        base.Setting();
        _player = FindAnyObjectByType<Player>();
        _core = FindAnyObjectByType<Core>();
    }

    public void EnemySetting(float hp, float speed, Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _hp = hp;
        _speed = speed;
    }

    public void Die()
    {
        _poolable.PushThisObject();
    }
}
