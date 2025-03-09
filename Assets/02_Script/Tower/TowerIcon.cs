using UnityEngine;

public class TowerIcon : BaseObject
{
    private PoolableObject _poolable;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.TowerIcon;
        transform.localScale = Vector3.one * 0.4f;

        return true;
    }

    public void TowerIconSetting(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
