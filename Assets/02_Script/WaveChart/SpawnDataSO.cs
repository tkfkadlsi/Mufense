using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SpawnData")]
public class SpawnDataSO : ScriptableObject
{
    public List<EnemySpawnData> SpawnData;
    private int count = 0;

    public void ResetCount()
    {
        count = 0;
    }

    public EnemySpawnData GetSpawnData()
    {
        if (count >= SpawnData.Count) return null;
        return SpawnData[count++];
    }
}