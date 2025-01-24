using System.Collections;
using UnityEngine;

public class Player : Unit, IMusicHandleObject
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;

    [Header("Dash")]
    [SerializeField] private int _dashCoolBeat;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Player;

        Managers.Instance.Game.InputReader.DashEvent += Dash;
        Managers.Instance.Game.BeatEvent += HandleMusicBeat;

        return true;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.InputReader.DashEvent -= Dash;
            Managers.Instance.Game.BeatEvent -= HandleMusicBeat;
        }

        base.Release();
    }

    private void Update()
    {
        Movement(Managers.Instance.Game.InputReader.MoveDirection);
    }

    private void Movement(Vector2 direction)
    {
        _rigidbody.linearVelocity = direction * _moveSpeed;
    }

    private void Dash()
    {
        if (_dashCoolBeat > 0)
            return;

        StartCoroutine(DashCoroutine());

        _dashCoolBeat = 4;
    }

    private IEnumerator DashCoroutine()
    {
        float t = 0f;
        float lerpTime = 60f / 120f;

        float originalSpeed = _moveSpeed;
        _moveSpeed *= 10f;

        while(t < lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            _moveSpeed = Mathf.Lerp(_moveSpeed, originalSpeed, t / lerpTime);
        }
    }

    public void HandleMusicBeat()
    {
        if(_dashCoolBeat > 0)
            _dashCoolBeat--;

        Managers.Instance.Pool.PopObject(PoolType.PlayerAttack, transform.position);
    }
}
