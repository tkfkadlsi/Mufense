using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum SoundEffect
{
    EnemyDie,
    Metronome,
}

public class SoundEffectPlayer : BaseInit
{
    [SerializedDictionary("Type", "SFX")] public SerializedDictionary<SoundEffect, AudioClip> _soundEffects;

    private AudioSource _effectPlayer;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _effectPlayer = GetComponent<AudioSource>();

        return true;
    }

    public void PlaySoundEffect(SoundEffect sfx)
    {
        _effectPlayer.PlayOneShot(_soundEffects[sfx]);
    }
}
