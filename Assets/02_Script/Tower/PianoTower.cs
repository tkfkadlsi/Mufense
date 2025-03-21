using UnityEngine;

public class PianoTower : Tower
{
    [SerializeField] private LayerMask _whatIsEnemy;

    private Collider2D[] _enemies = new Collider2D[10];
    private ContactFilter2D _filter = new ContactFilter2D();

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _filter.layerMask = _whatIsEnemy;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        TowerLevel = 1;
        Damage = 1;
        Range = 7;
    }

    private void Update()
    {
        if (_isStun) return;
        if (_target == null || _target.gameObject.activeSelf == false) return;

        Vector3 direction = _target.transform.position - transform.position;

        transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 5f);
    }

    private void SearchTarget()
    {
        int count = Physics2D.OverlapCircle(transform.position, Range, _filter, _enemies);
        if( count > 0 )
        {
            _target = _enemies[Random.Range(0, count)].GetComponent<Enemy>();
        }
    }

    protected override void HandleNoteEvent(TowerType type)
    {
        if(_isStun) return;
        if (type != TowerType.Piano) return;

        SearchTarget();

        int atkCount = Random.Range(TowerLevel, TowerLevel + 3);

        for(int i = 0; i < atkCount; i++)
        {
            PianoAttack attack = Managers.Instance.Pool.PopObject(PoolType.PianoAttack, transform.position).GetComponent<PianoAttack>();
            attack.transform.position += transform.right * Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
            attack.SettingTarget(transform.up, Damage, this);
        }
    }
}
