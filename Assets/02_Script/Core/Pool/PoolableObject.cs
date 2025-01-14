using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public PoolType poolType;

    public void PushThisObject()
    {
        Managers.Instance.Pool.PushObject(poolType, this);
    }
}
