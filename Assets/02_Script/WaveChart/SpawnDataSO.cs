using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SpawnData")]
public class SpawnDataSO : ScriptableObject
{

    [SerializeField] private List<BarSO> _inputSpawnData = new List<BarSO>();
    private List<EnemyType> _spawnData = new List<EnemyType>();
    private int count = 0;

    public void ResetCount()
    {
        _spawnData.Clear();
        foreach (var bar in _inputSpawnData)
        {
            for (int i = 0; i < 16; i++)
            {
                _spawnData.Add(bar.GetEnemyType(i));
            }
        }

        count = 0;
    }

    public PoolType GetSpawnData()
    {
        if (count >= _spawnData.Count) return PoolType.Null;
        switch (_spawnData[count++])
        {
            case EnemyType.Normal_1:
                return PoolType.Enemy;
            case EnemyType.Blink_2:
                return PoolType.BlinkEnemy;
            case EnemyType.Cancled_3:
                return PoolType.CancledEnemy;
            case EnemyType.HP_UP_10:
                Enemy.HPLevel++;
                return PoolType.Null;
            default:
                return PoolType.Null;
        }
    }

    public int SpawnDataCount => _spawnData.Count;
}