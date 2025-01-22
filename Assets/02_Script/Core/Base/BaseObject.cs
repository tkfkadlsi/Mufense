using UnityEngine;

public enum ObjectType
{
    Wall = 0,
    Core = 1,
    Enemy = 100,
    Player = 101,
    Attacks = 200
}

public class BaseObject : BaseInit
{
    protected SpriteRenderer _spriteRenderer;
    protected ObjectType _objectType;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();

        return true;
    }

    protected virtual void Setting()
    {
        _spriteRenderer.sortingOrder = (int)_objectType;
    }
}
