using UnityEngine;

public class PianoTower : Tower
{
    public int TowerLevel { get; set; }
    public float Damage {  get; set; }
    public float Range { get; set; }

    [SerializeField] private LayerMask _whatIsEnemy;
    private Enemy _target;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }



        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        TowerLevel = 1;
        Damage = 1;
        Range = 13;
    }

    private void Update()
    {
        if(_target == null || _target.gameObject.activeInHierarchy == false)
        {
            SearchTarget();
        }

        Vector3 direction = _target.transform.position - transform.position;

        transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 5f);
    }

    private void SearchTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll (transform.position, Range, _whatIsEnemy);
        _target = enemies[Random.Range(0, enemies.Length)].GetComponent<Enemy>();
    }

    protected override void HandleNoteEvent(TowerType type)
    {
        if (type != TowerType.Piano) return;

        int atkCount = Random.Range(TowerLevel, TowerLevel + 3);

        for(int i = 0; i < atkCount; i++)
        {
            PianoAttack attack = Managers.Instance.Pool.PopObject(PoolType.PianoAttack, transform.position).GetComponent<PianoAttack>();
            attack.transform.position += transform.right * Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
            attack.SettingTarget(transform.up, Damage);
        }
    }
}
