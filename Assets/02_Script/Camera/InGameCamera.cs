using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class InGameCamera : BaseInit, IMusicPlayHandle
{
    private CinemachineBasicMultiChannelPerlin _perlin;
    private CinemachineCamera _camera;

    private readonly float _zoomInSize = 3f;
    private readonly float _zoomOutSize = 11f;
    private readonly float _zoomTime = 1f;

    public bool IsLock { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        IsLock = false;

        _perlin = FindAnyObjectByType<CinemachineBasicMultiChannelPerlin>();
        _camera = FindAnyObjectByType<CinemachineCamera>();
        _perlin.AmplitudeGain = 0;
        _perlin.FrequencyGain = 0;

        return true;
    }

    public void SetCameraShake(float shakeStrenght)
    {
        _perlin.AmplitudeGain = shakeStrenght;
        _perlin.FrequencyGain = shakeStrenght;
    }

    public void CamLockToObject(Transform obj)
    {
        IsLock = true;
        Vector3 endPos = obj.position;
        endPos.x -= _zoomInSize * 0.888f;
        endPos.z = _camera.transform.position.z;
        StartCoroutine(CamSizeCoroutine(_zoomInSize));
        _camera.transform.DOMove(endPos, _zoomTime);
    }

    public void CamUnLock()
    {
        StartCoroutine(CamSizeCoroutine(_zoomOutSize));
        _camera.transform.DOMove(new Vector3(0, 0, _camera.transform.position.z), _zoomTime).OnComplete(() =>
        {
            IsLock = false;
        });
    }

    private IEnumerator CamSizeCoroutine(float endSize)
    {
        float t = 0f;
        float lerpTime = _zoomTime;

        float startSize = _camera.Lens.OrthographicSize;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _camera.Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, t / lerpTime);
        }

        _camera.Lens.OrthographicSize = endSize;
    }

    public void SettingColor(Music music)
    {
        Camera.main.DOColor(music.BackGroundColor, 1f);
    }
}
