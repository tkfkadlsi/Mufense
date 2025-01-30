using Unity.Cinemachine;
using UnityEngine;

public class InGameCamera : BaseInit
{
    private CinemachineCamera _cineCam;

    private Transform _playerTrm;
    private Transform _coreTrm;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _cineCam = GetComponent<CinemachineCamera>();

        _playerTrm = FindAnyObjectByType<Player>().transform;
        _coreTrm = FindAnyObjectByType<Core>().transform;

        Managers.Instance.Game.InputReader.FocusPlayerEvent += FocusPlayer;
        Managers.Instance.Game.InputReader.FocusCoreEvent += FocusCore;

        return true;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.InputReader.FocusPlayerEvent -= FocusPlayer;
            Managers.Instance.Game.InputReader.FocusCoreEvent -= FocusCore;
        }

        base.Release();
    }

    private void FocusPlayer()
    {
        _cineCam.Follow = _playerTrm;
    }

    private void FocusCore()
    {
        _cineCam.Follow = _coreTrm;
    }
}
