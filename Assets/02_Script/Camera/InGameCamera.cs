using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class InGameCamera : BaseInit, IMusicPlayHandle
{
    private Transform _targetTrm;

    private Transform _playerTrm;
    private Transform _coreTrm;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _playerTrm = FindAnyObjectByType<Player>().transform;
        _coreTrm = FindAnyObjectByType<Core>().transform;

        _targetTrm = _playerTrm;


        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.InputReader.FocusPlayerEvent += FocusPlayer;
        Managers.Instance.Game.InputReader.FocusCoreEvent += FocusCore;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    private void Update()
    {
        transform.position = _targetTrm.position;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.InputReader.FocusPlayerEvent -= FocusPlayer;
            Managers.Instance.Game.InputReader.FocusCoreEvent -= FocusCore;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }

    private void FocusPlayer()
    {
        _targetTrm = _playerTrm;
    }

    private void FocusCore()
    {
        _targetTrm = _coreTrm;
    }

    public void SettingColor(Music music)
    {
        Camera.main.DOColor(music.BackGroundColor, 1f);
    }
}
