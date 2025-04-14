using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct Bar
{
    [SerializeField] private EnemyType One_Beat;
    [SerializeField] private EnemyType Two_Beat;
    [SerializeField] private EnemyType Three_Beat;
    [SerializeField] private EnemyType Four_Beat;
    [SerializeField] private EnemyType Five_Beat;
    [SerializeField] private EnemyType Six_Beat;
    [SerializeField] private EnemyType Seven_Beat;
    [SerializeField] private EnemyType Eight_Beat;
    [SerializeField] private EnemyType Nine_Beat;
    [SerializeField] private EnemyType Ten_Beat;
    [SerializeField] private EnemyType Eleven_Beat;
    [SerializeField] private EnemyType Twelve_Beat;
    [SerializeField] private EnemyType Thirteen_Beat;
    [SerializeField] private EnemyType Fourteen_Beat;
    [SerializeField] private EnemyType Fifteen_Beat;
    [SerializeField] private EnemyType Sixteen_Beat;

    public EnemyType GetEnemyType(int index)
    {
        switch (index)
        {
            case 0: return One_Beat;
            case 1: return Two_Beat;
            case 2: return Three_Beat;
            case 3: return Four_Beat;
            case 4: return Five_Beat;
            case 5: return Six_Beat;
            case 6: return Seven_Beat;
            case 7: return Eight_Beat;
            case 8: return Nine_Beat;
            case 9: return Ten_Beat;
            case 10: return Eleven_Beat;
            case 11: return Twelve_Beat;
            case 12: return Thirteen_Beat;
            case 13: return Fourteen_Beat;
            case 14: return Fifteen_Beat;
            case 15: return Sixteen_Beat;
            default: return EnemyType.None_0;
        }
    }
}

[CreateAssetMenu(menuName = "SO/SpawnData")]
public class SpawnDataSO : ScriptableObject
{

    [SerializeField] private List<Bar> _inputSpawnData = new List<Bar>();
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
            default:
                return PoolType.Null;
        }
    }

    public int SpawnDataCount => _spawnData.Count;
}