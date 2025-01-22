using UnityEngine;

public class Player : Unit
{
    [SerializeField] private float _moveSpeed;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Player;

        return true;
    }

    private void Update()
    {
        Movement(Managers.Instance.Game.InputReader.MoveDirection);
    }


    private void Movement(Vector2 direction)
    {
        _rigidbody.linearVelocity = direction * _moveSpeed * Time.deltaTime;
    }
}
