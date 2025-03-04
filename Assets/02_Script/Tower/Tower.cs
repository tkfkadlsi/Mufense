using DG.Tweening;
using UnityEngine;

public enum TowerType
{
    None = 0,
    Normal = 1,
    Line = 2,
    Star = 3,
    Bomb = 4
}

public class Tower : BaseObject, IMusicHandleObject, IMusicPlayHandle
{
    public static int PlusDamage = 0;
    [SerializeField] private LayerMask _whatIsEnemy;

    private Enemy _target;
    private TowerType _type;
    private TowerIcon _towerIcon;
    private PoolableObject _poolable;
    private CircleCollider2D _circleCollider;

    private int _attackCooltime;
    private int _cooldown;
    private float _range;
    private int _life;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return true;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.Tower;
        _circleCollider = GetComponent<CircleCollider2D>();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return false;
    }

    protected override void Setting()
    {
        base.Setting();
        _towerIcon = Managers.Instance.Pool.PopObject(PoolType.TowerIcon, transform.position).GetComponent<TowerIcon>();
        _circleCollider.radius = 0.71f;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        Managers.Instance.Game.BeatEvent += HandleMusicBeat;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }

    private void OnDisable()
    {
        if(_towerIcon != null)
            _towerIcon.PushThisObject();
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= HandleMusicBeat;
        }
    }

    public void TowerSetting(TowerType type, Sprite sprite, int attackCooltime, float range)
    {
        if(type == TowerType.None)
        {
            _poolable.PushThisObject();
        }

        if(type == TowerType.Bomb)
        {
            _towerIcon.transform.localScale = Vector3.one * 0.75f;
        }
        else
        {
            _towerIcon.transform.localScale = Vector3.one;
        }

        _type = type;
        _attackCooltime = attackCooltime;
        _cooldown = attackCooltime;
        _range = range;
        _towerIcon.TowerIconSetting(sprite);

        _life = 100;
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

        if(_life == 0)
        {
            _poolable.PushThisObject();
        }
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _range, (int)_whatIsEnemy);

        if(enemies.Length == 0)
        {
            return;
        }

        _target = enemies[Random.Range(0, enemies.Length)].GetComponent<Enemy>();
        _life--;

        //타겟과 시작 위치를 매개변수로 공격.

        switch(_type)
        {
            case TowerType.Normal:
                TowerAttack normalTowerAttack = Managers.Instance.Pool.PopObject(PoolType.NormalTowerAttack, transform.position).GetComponent<TowerAttack>();
                normalTowerAttack.SettingTarget(_target.transform, (int)TowerType.Normal + PlusDamage);
                break;

            case TowerType.Line:
                TowerAttack lineTowerAttack = Managers.Instance.Pool.PopObject(PoolType.LineTowerAttack, transform.position).GetComponent<TowerAttack>();
                lineTowerAttack.SettingTarget(_target.transform, (int)TowerType.Line + PlusDamage);
                break;

            case TowerType.Star:
                TowerAttack starTowerAttack = Managers.Instance.Pool.PopObject(PoolType.StarTowerAttack, transform.position).GetComponent<TowerAttack>();
                starTowerAttack.SettingTarget(_target.transform, (int)TowerType.Star + PlusDamage);
                break;

            case TowerType.Bomb:
                TowerAttack bombTowerAttack = Managers.Instance.Pool.PopObject(PoolType.BombTowerAttack, transform.position).GetComponent<TowerAttack>();
                bombTowerAttack.SettingTarget(_target.transform, (int)TowerType.Bomb + PlusDamage);
                break;
        }
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }
}
