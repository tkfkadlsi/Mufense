using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayObject : BaseInit
{
    public List<Way> Ways = new List<Way>();
    public List<List<EnemyDataInWave>> EnemyDataList = new List<List<EnemyDataInWave>>();
    public List<EnemySpawner> EnemySpawnerList = new List<EnemySpawner>();

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        foreach(Way way in Ways)
        {
            foreach (Transform transform in way.way)
            {
                transform.GetComponent<SpriteRenderer>().sortingOrder = (int)ObjectType.Way;
            }
        }

        return true;
    }

    public void SettingEnemySpawner()
    {
        if(EnemySpawnerList.Count > 0)
        {
            EnemyDataList.Clear();
        }

        for(int i = 0; i < Ways.Count; i++)
        {
            EnemySpawner enemySpawner 
                = Managers.Instance.Pool.PopObject(PoolType.EnemySpawner, Ways[i].way[0].position).GetComponent<EnemySpawner>();
            EnemySpawnerList.Add(enemySpawner);
        }
    }

    public void SpawnEnemy(int index)
    {
        for(int i = 0; i <  EnemyDataList.Count; i++)
        {
            EnemySpawnerList[i].SpawnEnemy(EnemyDataList[i][index].PoolType);
        }
    }

    public void ReleaseEnemySpawner()
    {
        while(EnemySpawnerList.Count > 0)
        {
            EnemySpawner enemySpawner = EnemySpawnerList[0];
            EnemySpawnerList.Remove(enemySpawner);
            enemySpawner.PushThisObject();
        }
    }

    public Vector3 GetTargetPosition(int wayNumber, int index)
    {
        return Ways[wayNumber].GetPosition(index);
    }
}
