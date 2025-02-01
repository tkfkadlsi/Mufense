using UnityEngine;

public abstract class Unit : BaseObject
{
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;


    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _rigidbody = gameObject.GetOrAddComponent<Rigidbody2D>();
        _collider = gameObject.GetOrAddComponent<Collider2D>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _rigidbody.gravityScale = 0;
        _rigidbody.linearDamping = 1;
        _rigidbody.angularDamping = 1;
        _collider.isTrigger = false;
    }
}
