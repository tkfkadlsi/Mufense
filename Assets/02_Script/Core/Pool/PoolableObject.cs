using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public PoolType poolType;

    public void PushThisObject()
    {
        if (Managers.Instance != null)
            Managers.Instance.Pool.PushObject(poolType, this);
    }
}
