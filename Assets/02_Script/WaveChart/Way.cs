using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Way
{
    public List<Transform> way;

    public Vector3 GetPosition(int index)
    {
        return way[index].position;
    }
}
