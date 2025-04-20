using UnityEngine;

public class StringTower : Tower
{
    [SerializeField] private LayerMask _whatIsEnemy;

    private Collider2D[] _enemies = new Collider2D[10];
    private ContactFilter2D _filter = new ContactFilter2D();

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _filter.layerMask = _whatIsEnemy;
        _filter.useTriggers = true;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        TowerLevel = 1;
        Range = 11;
        Damage = 1;

        _target = null;
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
        (Enemy, float) distance = (null, float.MaxValue);
        Enemy[] enemies = FindObjectsByType<Enemy>(sortMode: FindObjectsSortMode.None);
        foreach (Enemy e in enemies)
        {
            float d = Vector3.Distance(e.transform.position, transform.position);

            if (distance.Item2 > d)
            {
                distance = (e, d);
            }
        }

        _target = distance.Item1 == null ? null : distance.Item1;
    }

    protected override void HandleNoteEvent(TowerType type)
    {
        if (_isStun) return;
        if (type != TowerType.String) return;

        SearchTarget();

        StringAttack attack = Managers.Instance.Pool.PopObject(PoolType.StringAttack, transform.position).GetComponent<StringAttack>();
        attack.SettingTarget(transform.up, Damage, this);
    }
}
