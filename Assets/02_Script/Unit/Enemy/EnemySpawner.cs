using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BaseInit, IMusicHandleObject
{
    [SerializeField] private List<EnemyStat> _enemyStatList;

    private float _squareEnemyPercent = 5f;
    private float _fentagonEnemyPercent = 1.5f;

    private int _spawnCooltime;
    private int _spawnBeatCounter;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _spawnCooltime = 8;
        _spawnBeatCounter = 8;

        Managers.Instance.Game.BeatEvent += HandleMusicBeat;

        return true;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
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

    public void HandleMusicBeat()
    {
        if(_spawnBeatCounter >= _spawnCooltime)
        {
            _spawnBeatCounter = 0;
            SpawnEnemy();
        }
        _spawnBeatCounter++;
    }

    private void SpawnEnemy()
    {
        float rand = Random.Range(0f, 100f);
        int spawnNumber = 0;
        if(rand <= _fentagonEnemyPercent * Managers.Instance.Game.SongCount)
        {
            spawnNumber = 2;
        }
        else if(rand <= _squareEnemyPercent * Managers.Instance.Game.SongCount)
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
