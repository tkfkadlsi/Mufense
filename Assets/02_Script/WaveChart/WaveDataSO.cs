using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveDataSO : ScriptableObject, IMusicHandleObject
{
    public event Action WaveClearEvent;
    public WayObject WayObject;
    public float NormalEnemyHP;
    public float BlinkEnemyHP;
    public float CancledEnemyHP;

    protected List<Enemy> _enemyList = new List<Enemy>();

    private List<EnemySpawner> _enemySpawnerList = new List<EnemySpawner>();
    private int _enemySpawnIndex;

    public virtual void StartWave()
    {
        if(_enemyList.Count > 0)
        {
            _enemyList.Clear();
        }
        if(_enemySpawnerList.Count > 0)
        {
            _enemySpawnerList.Clear();
        }

        _enemySpawnIndex = 0;
        WayObject.SettingEnemySpawner();
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
    }

    public virtual void StopWave()
    {
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;


        while(_enemyList.Count > 0)
        {
            Enemy enemy = _enemyList[0];
            _enemyList.Remove(enemy);
            enemy.PushThisObject();
        }

        WayObject.ReleaseEnemySpawner();
    }

    public void HandleDeathEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        if(_enemyList.Count == 0)
        {
            WaveClearEvent?.Invoke();
        }
    }

    public void HandleMusicBeat()
    {
        WayObject.SpawnEnemy(_enemySpawnIndex);
        _enemySpawnIndex++;
    }
}
