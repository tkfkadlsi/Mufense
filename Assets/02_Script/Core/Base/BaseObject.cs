using UnityEngine;

public enum ObjectType
{
    BackGround = 0,
    MusicPowerOrb = 1,
    Effect = 2,
    Attacks = 3,
    Tower = 100,
    TowerIcon = 101,
    TowerGuide = 102,
    Enemy = 200,
    Player = 201,
    Treasure = 210,
    Core = 300,
    FrontEffect = 999
}

public abstract class BaseObject : BaseInit
{
    protected SpriteRenderer _spriteRenderer;
    protected ObjectType _objectType;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _spriteRenderer.sortingOrder = (int)_objectType;
    }
}
