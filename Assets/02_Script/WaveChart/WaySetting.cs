using UnityEngine;
using System.Collections.Generic;

public class WaySetting : BaseInit
{
    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Way[] ways = GetComponentsInChildren<Way>();

        for(int i = 0; i < ways.Length - 1; i++)
        {
            ways[i].SetNextWay(ways[i + 1]);
        }

        return true;
    }
}
