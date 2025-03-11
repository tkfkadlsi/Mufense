using UnityEngine;
using UnityEngine.Rendering;

public class StringTower : Tower
{
    [SerializeField] private LayerMask _whatIsEnemy;

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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, Range, _whatIsEnemy);
        if (enemies.Length > 0)
        {
            _target = enemies[Random.Range(0, enemies.Length)].GetComponent<Enemy>();
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
