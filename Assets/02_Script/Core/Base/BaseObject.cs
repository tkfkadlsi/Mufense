using UnityEngine;

public enum ObjectType
{
    Wall = 0,
    MusicPowerOrb = 1,
    Effect = 2,
    Attacks = 3,
    Treasure = 10,
    Enemy = 100,
    Player = 101,
    Tower = 200,
    TowerIcon = 201,
    TowerGuide = 202,
    Core = 300,
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

    protected override void Setting()
    {
        _spriteRenderer.sortingOrder = (int)_objectType;
    }
}
