using UnityEngine;

public class Unit : BaseObject
{
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;


    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _rigidbody.gravityScale = 0;
        _collider.isTrigger = false;
    }
}
