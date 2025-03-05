using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : BaseInit, IMusicHandleObject
{
    private int _spawnCount;
    private int _spawnCooltime;

    private int _cooldown;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        

        return true;
    }

    public void SettingSpawnInfo(int spawnCount, int spawnCooltime)
    {
        _spawnCount = spawnCount;
        _spawnCooltime = spawnCooltime;
        _cooldown = spawnCooltime;
    }

    public void HandleMusicBeat()
    {
        if(_cooldown > _spawnCooltime)
        {
            _cooldown -= _spawnCooltime;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {

    }
}
