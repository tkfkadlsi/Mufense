using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public AudioMixer AudioMixer;

    #region Events

    public Action<Language> ChangeLanguageEvent;

    #endregion

    #region Option

    public Language Language;

    public float MasterVolume;
    public float MusicVolume;
    public float EffectVolume;

    #endregion
}
