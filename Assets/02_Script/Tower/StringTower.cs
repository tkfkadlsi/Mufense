using UnityEngine;
using UnityEngine.Rendering;

public class StringTower : Tower
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
        Range = 9;
        Damage = 1;
    }

    private void Update()
    {
        if (_target == null || _target.gameObject.activeSelf == false) return;

        Vector3 direction = _target.transform.position - transform.position;

        transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 5f);
    }

    private void SearchTarget()
    {
        int count = Physics2D.OverlapCircle(transform.position, Range, _filter, _enemies);
        if (count > 0)
        {
            _target = _enemies[Random.Range(0, count)].GetComponent<Enemy>();
        }
    }

    protected override void HandleNoteEvent(TowerType type)
    {
        if (type != TowerType.String) return;

        SearchTarget();

        StringAttack attack = Managers.Instance.Pool.PopObject(PoolType.StringAttack, transform.position).GetComponent<StringAttack>();
        attack.SettingTarget(transform.up, Damage);
    }
}
