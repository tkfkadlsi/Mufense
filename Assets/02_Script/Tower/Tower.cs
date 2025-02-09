using UnityEngine;

public enum TowerType
{
    Normal,
    Line,
    Star,
    Bomb
}

public class Tower : BaseObject, IMusicHandleObject
{
    private Enemy _target;
    private TowerType _type;
    private TowerIcon _towerIcon;
    private PoolableObject _poolable;

    private int _attackCooltime;
    private int _cooldown;
    private float _range;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return true;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Tower;

        return false;
    }

    protected override void Setting()
    {
        base.Setting();
        _towerIcon = Managers.Instance.Pool.PopObject(PoolType.TowerIcon, transform.position).GetComponent<TowerIcon>();
    }

    private void OnDisable()
    {
        _towerIcon.PushThisObject();
    }

    public void TowerSetting(TowerType type, Sprite sprite, int attackCooltime, float range)
    {
        _type = type;
        _towerIcon.TowerIconSetting(sprite);
        _attackCooltime = attackCooltime;
        _cooldown = attackCooltime;
        _range = range;
    }

    private void Update()
    {
        transform.Rotate(0, 0, ((360f / _attackCooltime) / Managers.Instance.Game.UnitTime) * Time.deltaTime);
    }

    public void HandleMusicBeat()
    {
        if (_cooldown >= _attackCooltime)
        {
            _cooldown = 0;
            Attack();
        }
        _cooldown++;
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _range);
        _target = enemies[Random.Range(0, enemies.Length)].GetComponent<Enemy>();

        //타겟과 시작 위치를 매개변수로 공격.

        switch(_type)
        {
            case TowerType.Normal:
                break;

            case TowerType.Line:
                break;

            case TowerType.Star:
                break;

            case TowerType.Bomb:
                break;
        }
    }
}
