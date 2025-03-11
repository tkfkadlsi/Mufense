using UnityEngine;

public class PianoTower : Tower
{
    [SerializeField] private LayerMask _whatIsEnemy;

    protected override void Setting()
    {
        base.Setting();

        TowerLevel = 1;
        Damage = 1;
        Range = 7;
    }

    private void Update()
    {
        if (_target == null || _target.gameObject.activeSelf == false) return;

        Vector3 direction = _target.transform.position - transform.position;

        transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 5f);
    }

    private void SearchTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll (transform.position, Range, _whatIsEnemy);
        if( enemies.Length > 0 )
        {
            _target = enemies[Random.Range(0, enemies.Length)].GetComponent<Enemy>();
        }
    }

    protected override void HandleNoteEvent(TowerType type)
    {
        if (type != TowerType.Piano) return;

        SearchTarget();

        int atkCount = Random.Range(TowerLevel, TowerLevel + 3);

        for(int i = 0; i < atkCount; i++)
        {
            PianoAttack attack = Managers.Instance.Pool.PopObject(PoolType.PianoAttack, transform.position).GetComponent<PianoAttack>();
            attack.transform.position += transform.right * Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
            attack.SettingTarget(transform.up, Damage);
        }
    }
}
