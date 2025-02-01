using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Create,
    Active,
    Death
}

public class Enemy : Unit, IHealth
{
    private EnemyState _state;
    private float _speed;
    private float _originSpeed;

    private Player _player;
    private Core _core;

    private Transform _target;
    private PoolableObject _poolable;

    private IEnumerator HitedCoroutine;

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
            else
            {
                if(HitedCoroutine is not null)
                {
                    StopCoroutine(HitedCoroutine);
                }
                HitedCoroutine = Hited();
                StartCoroutine(HitedCoroutine);
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
        if (_state != EnemyState.Active) return;
        if(4f >= Vector3.Distance(_player.transform.position, transform.position))
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
        _state = EnemyState.Create;
        _player = FindAnyObjectByType<Player>();
        _core = FindAnyObjectByType<Core>();
    }

    public void EnemySetting(float hp, float speed, Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _hp = hp;
        _speed = speed;
        _originSpeed = speed;
        _state = EnemyState.Active;
    }

    public void Die()
    {
        _state = EnemyState.Death;
        //���̾Ƹ�� ������ ����ϰ� �ڵ����� �ھ�� �������� �ؼ� �ھ ��� ���� �����Ŀ� ��� ����.
        _poolable.PushThisObject();
    }

    public IEnumerator Hited()
    {
        float t = 0f;
        float lerpTime = 1f;

        while (t < lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            _speed = Mathf.Lerp(0f, _originSpeed, t / lerpTime);
        }
    }
}
