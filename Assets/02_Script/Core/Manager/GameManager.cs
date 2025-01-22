using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public int SongCount = 0;
    public Music PlayingMuisc;

    #region Function

    private Dictionary<Type, BaseInit> _scriptDict = new Dictionary<Type, BaseInit>();

    public T FindBaseInitScript<T>() where T : BaseInit
    {
        if(_scriptDict.ContainsKey(typeof(T)) == false)
        {
            _scriptDict.Add(typeof(T), FindAnyObjectByType<T>());
        }

        return _scriptDict[typeof(T)] as T;
    }

    public void SongFinish()
    {
        
    }

    #endregion

    #region Events

    public Action<Language> ChangeLanguageEvent;

    #endregion

    #region Option

    public Language Language;

    public float MasterVolume;
    public float MusicVolume;
    public float EffectVolume;

    public bool AutoStartSong;
    public bool LowDetailMod;

    #endregion
}
