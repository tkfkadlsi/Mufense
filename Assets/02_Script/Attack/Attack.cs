using UnityEngine;

public abstract class Attack : BaseObject
{
    private Collider2D _collider;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Attacks;

        _collider = gameObject.GetOrAddComponent<Collider2D>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _collider.isTrigger = true;
    }
}
