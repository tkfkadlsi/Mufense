using System.Collections;
using UnityEngine;

public class CoreNoise : BaseInit
{
    [SerializeField] private AudioClip _noiseClip;

    private Core _core;

    private int _noiseLevel = 0;
    private AudioSource _audioSource;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _core = GetComponent<Core>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.mute = true;
        _audioSource.clip = _noiseClip;
        _audioSource.volume = 0;
        _audioSource.Play();
        StartCoroutine(NoiseCoroutine());
        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _core.HPChangeEvent += CheckNoiseLevel;
    }

    protected override void Release()
    {
        _core.HPChangeEvent -= CheckNoiseLevel;

        base.Release();
    }

    private void CheckNoiseLevel(float hp)
    {
        if (hp > 75f)
        {
            _noiseLevel = 0;
            _audioSource.volume = 0;
        }
        else if (hp > 50f)
        {
            _noiseLevel = 1;
            _audioSource.volume = 0.25f;
        }
        else if (hp > 25f)
        {
            _noiseLevel = 2;
            _audioSource.volume = 0.5f;
        }
        else if (hp > 10f)
        {
            _noiseLevel = 3;
            _audioSource.volume = 0.625f;
        }
        else
        {
            _noiseLevel = 4;
            _audioSource.volume = 0.75f;
        }
    }

    private IEnumerator NoiseCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => _noiseLevel > 0);

            _audioSource.time = Random.Range(1f, 15f);
            _audioSource.mute = false;
            Managers.Instance.Game.FindBaseInitScript<InGameCamera>().SetCameraShake(_noiseLevel);
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().SetMute(true);

            yield return new WaitForSeconds(Random.Range(_noiseLevel * 0.1f, _noiseLevel * 0.3f));

            _audioSource.mute = true;
            Managers.Instance.Game.FindBaseInitScript<InGameCamera>().SetCameraShake(0);
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().SetMute(false);

            yield return new WaitForSeconds(Random.Range(3f / _noiseLevel, 5f / _noiseLevel));
        }
    }
}
