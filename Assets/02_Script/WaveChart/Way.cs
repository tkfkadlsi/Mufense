using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Way : BaseObject
{
    [SerializeField] private Way _nextWay;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Way;

        return true;
    }

    public Way GetNextWay()
    {
        return _nextWay;
    }
}
