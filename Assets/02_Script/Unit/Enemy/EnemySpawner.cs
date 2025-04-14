using System;
using UnityEngine;

public class EnemySpawner : BaseInit, IMusicHandleObject
{
    public static Action AllEnemyDieEvent;
    private static int _savedEnemyCount;
    private static bool _reset = false;

    public static int SavedEnemyCount
    {
        get
        {
            return _savedEnemyCount;
        }
        set
        {
            _savedEnemyCount = value;
            if(_savedEnemyCount <= 0)
            {
                AllEnemyDieEvent?.Invoke();
            }
        }
    }

    [SerializeField] private SpawnDataSO _spawnData;
    [SerializeField] private Way _startWay;
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        if(_reset == false)
        {
            _reset = true;
            _savedEnemyCount = 0;
        }

        transform.position = _startWay.transform.position;
        _spawnData.ResetCount();
        _savedEnemyCount += _spawnData.SpawnDataCount;
        _poolable = GetComponent<PoolableObject>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;
        }

        base.Release();
    }

    public void SpawnEnemy(PoolType type, Way way)
    {
        Managers.Instance.Pool.PopObject(type, transform.position).GetComponent<Enemy>().EnemySetting(way);
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }

    public void HandleMusicBeat()
    {
        PoolType type = _spawnData.GetSpawnData();
        if (type == PoolType.Null) return;
        SpawnEnemy(type, _startWay);
    }
}
