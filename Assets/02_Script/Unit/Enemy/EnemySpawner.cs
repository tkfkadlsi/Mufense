using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : BaseInit, IMusicHandleObject
{
    private readonly int _spawnCooltime = 4;

    private int _cooldown;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _cooldown = _spawnCooltime;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;
        }

        base.Release();
    }

    public void HandleMusicBeat()
    {
        _cooldown++;
        if (_cooldown > _spawnCooltime)
        {
            _cooldown -= _spawnCooltime;

            for (int i = 0; i < 4 + Managers.Instance.Game.FindBaseInitScript<GameTimer>().EnemySpawnAmountLevel * 1; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        int rand = 0;

        switch (Managers.Instance.Game.FindBaseInitScript<GameTimer>().EnemySpawnLevel)
        {
            case 1:
                rand = 1;
                break;
            case 2:
                rand = Random.Range(1, 10); //1부터 9
                break;
            case 3:
                rand = Random.Range(1, 11); //1부터 10
                break;
        }

        if(rand >= 10)
        {
            Managers.Instance.Pool.PopObject(PoolType.CancledEnemy, direction.normalized * 30f);
        }
        else if(rand >= 8)
        {
            Managers.Instance.Pool.PopObject(PoolType.BlinkEnemy, direction.normalized * 30f);
        }
        else
        {
            Managers.Instance.Pool.PopObject(PoolType.Enemy, direction.normalized * 30f);
        }
    }
}
