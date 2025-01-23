using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    TriangleEnemy,
    PlayerAttack
}

public class PoolManager : MonoBehaviour
{
    [SerializedDictionary("PoolType", "Object")]
    public SerializedDictionary<PoolType, PoolableObject> SettingPoolObjectDict = new SerializedDictionary<PoolType, PoolableObject>();

    private Dictionary<PoolType, Pool> _pools = new Dictionary<PoolType, Pool>();

    public void Init()
    {
        foreach (var setting in SettingPoolObjectDict)
        {
            Pool pool = new Pool();
            
            _pools.Add(setting.Key, pool);
        }
    }

    private void CreatePO(PoolType poolType)
    {
        PoolableObject po = Instantiate(SettingPoolObjectDict[poolType]);
        po.poolType = poolType;
        po.PushThisObject();
    }

    public PoolableObject PopObject(PoolType poolType, Vector3 position)
    {
        if(_pools[poolType].PoolCount <= 0)
        {
            CreatePO(poolType);
        }

        return _pools[poolType].PopObject(position);
    }

    public PoolableObject PopObject(PoolType poolType, Vector3 position, Transform parent)
    {
        if (_pools[poolType].PoolCount <= 0)
        {
            CreatePO(poolType);
        }

        return _pools[poolType].PopObject(position, parent);
    }

    public void PushObject(PoolType poolType, PoolableObject obj)
    {
        _pools[poolType].PushObject(obj);
    }
}