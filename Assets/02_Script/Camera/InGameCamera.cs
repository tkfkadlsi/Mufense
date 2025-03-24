using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class InGameCamera : BaseInit, IMusicPlayHandle
{
    private CinemachineBasicMultiChannelPerlin _perlin;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _perlin = FindAnyObjectByType<CinemachineBasicMultiChannelPerlin>();
        _perlin.AmplitudeGain = 0;
        _perlin.FrequencyGain = 0;

        return true;
    }

    public void SetCameraShake(float shakeStrenght)
    {
        _perlin.AmplitudeGain = shakeStrenght;
        _perlin.FrequencyGain = shakeStrenght;
    }

    public void SettingColor(Music music)
    {
        Camera.main.DOColor(music.BackGroundColor, 1f);
    }
}
