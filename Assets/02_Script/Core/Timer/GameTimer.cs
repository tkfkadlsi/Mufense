using UnityEngine;

public class GameTimer : BaseInit
{
    public int EnemyHPLevel { get; private set; }
    public int EnemySpawnAmountLevel { get; private set; }
    public int EnemySpawnLevel { get; private set; }


    private readonly float _hpLevelCooltime = 120.0f;
    private readonly float _amountLevelCooltime = 230.0f;
    private float _spawnLevelCooltime = 10.0f;

    private readonly float _treasureSpawnCooltime = 40.0f;


    private float _hpLevelCooldown;
    private float _amountLevelCooldown;
    private float _spawnLevelCooldown;


    private float _treasureSpawnCooldown;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        EnemyHPLevel = 0;
        EnemySpawnAmountLevel = 0;
        EnemySpawnLevel = 1;

        return true;
    }

    private void Update()
    {
        _hpLevelCooldown += Time.deltaTime;
        _amountLevelCooldown += Time.deltaTime;
        _treasureSpawnCooldown += Time.deltaTime;
        _spawnLevelCooldown += Time.deltaTime;

        if (_hpLevelCooldown >= _hpLevelCooltime)
        {
            _hpLevelCooldown -= _hpLevelCooltime;
            EnemyHPLevel++;
        }
        if (EnemySpawnAmountLevel < 5 && _amountLevelCooldown >= _amountLevelCooltime)
        {
            _amountLevelCooldown -= _amountLevelCooltime;
            EnemySpawnAmountLevel++;
        }
        if (_treasureSpawnCooldown >= _treasureSpawnCooltime)
        {
            _treasureSpawnCooldown -= _treasureSpawnCooltime;

            if (Random.Range(-1f, 0.5f) >= 0)
            {
                Managers.Instance.Pool.PopObject(PoolType.MusicPowerTreasure, Vector3.zero);
            }
            else
            {
                Managers.Instance.Pool.PopObject(PoolType.SpecialTreasure, Vector3.zero);
            }
        }
        if (EnemySpawnLevel < 3 && _spawnLevelCooldown >= _spawnLevelCooltime)
        {
            _spawnLevelCooldown -= _spawnLevelCooltime;
            EnemySpawnLevel++;
            _spawnLevelCooltime *= 2;
        }
    }

    public float GetHPLevelCooldown()
    {
        return _hpLevelCooltime - _hpLevelCooldown;
    }

    public float GetSpawnAmountCooldown()
    {
        if (EnemySpawnAmountLevel >= 5) return float.MaxValue;
        return _amountLevelCooltime - _amountLevelCooldown;
    }

    public float GetSpawnLevelCooldown()
    {
        if (EnemySpawnLevel >= 3) return float.MaxValue;
        return _spawnLevelCooltime - _spawnLevelCooldown;
    }

    public float GetTreasureCooldown()
    {
        return _treasureSpawnCooltime - _treasureSpawnCooldown;
    }
}
