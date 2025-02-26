using UnityEngine;

public enum ObjectType
{
    Wall = 0,
    Core = 1,
    MusicPowerOrb = 10,
    Tower = 11,
    TowerIcon = 12,
    TowerGuide = 13,
    Enemy = 101,
    Player = 102,
    Attacks = 200,
}

public abstract class BaseObject : BaseInit
{
    protected SpriteRenderer _spriteRenderer;
    protected ObjectType _objectType;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();

        return true;
    }

    protected void OnEnable()
    {
        Setting();
    }

    protected virtual void Setting()
    {
        _spriteRenderer.sortingOrder = (int)_objectType;
    }
}
