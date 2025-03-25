using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : Unit, IMusicHandleObject, IMusicPlayHandle
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;

    [Header("Dash")]
    [SerializeField] private int _dashCoolBeat;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Player;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.InputReader.DashEvent += Dash;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.InputReader.DashEvent -= Dash;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }

    private void Update()
    {
        Movement(Managers.Instance.Game.InputReader.MoveDirection);
    }

    private void Movement(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            transform.localScale = Vector2.one;
        }
        else
        {
            transform.localScale = new Vector3(0.9f, 1.1f, 1f);
            transform.up = direction;
        }
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

        while (t < lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            _moveSpeed = Mathf.Lerp(_moveSpeed, originalSpeed, t / lerpTime);
        }
    }

    public void HandleMusicBeat()
    {
        if (_dashCoolBeat > 0)
            _dashCoolBeat--;
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }
}
