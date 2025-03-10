using UnityEngine;

public class TowerIcon : BaseObject
{
    private PoolableObject _poolable;
    private Tower _owner;

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

    private void Update()
    {
        transform.up = _owner.transform.up;
    }

    public void TowerIconSetting(Sprite sprite, Tower owner)
    {
        _spriteRenderer.sprite = sprite;
        _owner = owner;
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}
