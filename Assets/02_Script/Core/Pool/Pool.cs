using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pool
{
    private Stack<PoolableObject> _objectStack = new Stack<PoolableObject>();

    public int PoolCount => _objectStack.Count;

    public void PushObject(PoolableObject po)
    {
        po.gameObject.SetActive(false);
        po.transform.SetParent(null);

        po.transform.position = new Vector3(0, 0, -100);

        _objectStack.Push(po);
    }

    public PoolableObject PopObject(Vector3 position)
    {
        PoolableObject po = _objectStack.Pop();

        po.transform.position = position;

        po.gameObject.SetActive(true);
        return po;
    }

    public PoolableObject PopObject(Vector3 position, Transform parent)
    {
        PoolableObject po = _objectStack.Pop();

        po.transform.position = position;
        po.transform.SetParent(parent);

        po.gameObject.SetActive(true);
        return po;
    }
}
