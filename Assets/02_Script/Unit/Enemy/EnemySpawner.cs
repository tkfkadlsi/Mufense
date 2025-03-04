using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : BaseInit, IMusicHandleObject
{
    [SerializeField] private List<EnemyStat> _enemyStatList = new List<EnemyStat>();

    private float _squareEnemyPercent = 5f;
    private float _fentagonEnemyPercent = 1.5f;

    private int _spawnCooltime;
    private int _spawnBeatCounter;
    private int _spawnEnemyCount;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _spawnCooltime = 2;
        _spawnBeatCounter = 2;
        _spawnEnemyCount = 1;

        Managers.Instance.Game.BeatEvent += HandleMusicBeat;

        return true;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= HandleMusicBeat;
        }

        base.Release();
    }

    public void SetSpawnCoolTime(int spawnCooltime)
    {
        _spawnCooltime = spawnCooltime;
        _spawnBeatCounter = spawnCooltime;
    }

    public void SetSpawnCount(int spawnCount)
    {
        _spawnEnemyCount = spawnCount;
    }

    public void HandleMusicBeat()
    {
        if (_spawnBeatCounter >= _spawnCooltime)
        {
            _spawnBeatCounter = 0;
            for (int i = 0; i < _spawnEnemyCount; i++)
            {
                SpawnEnemy();
            }
        }
        _spawnBeatCounter++;
    }

    private void SpawnEnemy()
    {
        transform.position = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        if(Mathf.Abs(transform.position.x) < 5f && Mathf.Abs(transform.position.y) < 5f)
        {
            transform.position *= 5f;
        }


        float rand = Random.Range(0f, 100f);
        int spawnNumber = 0;
        if (rand <= _fentagonEnemyPercent * Managers.Instance.Game.SongCount)
        {
            spawnNumber = 2;
        }
        else if (rand <= _squareEnemyPercent * Managers.Instance.Game.SongCount)
        {
            spawnNumber = 1;
        }
        else
        {
            spawnNumber = 0;
        }

        Enemy newEnemy = Managers.Instance.Pool.PopObject(PoolType.Enemy, transform.position).GetComponent<Enemy>();

        newEnemy.EnemySetting(
            _enemyStatList[spawnNumber]._hp * Managers.Instance.Game.SongCount,
            _enemyStatList[spawnNumber]._speed,
            _enemyStatList[spawnNumber]._sprite);
    }
}
