using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlayer : BaseInit
{
    public List<Music> MusicList = new List<Music>();
    public event Action<Music> PlayMusic;

    private AudioSource _audioSource;
    private Dictionary<string, List<float>> _beatTimingsInSong = new Dictionary<string, List<float>>();
    private Dictionary<string, List<float>> _bpmTimingsInSong = new Dictionary<string, List<float>>();
    private int beatCounter = 0;
    private int bpmCounter = 0;
    private Music PlayingMusic;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _audioSource = GetComponent<AudioSource>();

        foreach(Music music in MusicList)
        {
            _beatTimingsInSong.Add(music.SongName, new List<float>());
            _bpmTimingsInSong.Add(music.SongName, new List<float>());

            MakeBeatTiming(music);
        }

        return true;
    }

    private void MakeBeatTiming(Music music)
    {
        List<float> timings = new List<float>();
        foreach(var bcd in music.BpmChangeDict)
        {
            timings.Add(bcd.Key);
            _bpmTimingsInSong[music.SongName].Add(bcd.Key);
        }

        float timing = 0f;
        float unitTime = 0f;

        for(int i = 0; i < timings.Count; i++)
        {
            timing = timings[i];
            unitTime = 60f / music.BpmChangeDict[timings[i]];
        
            while(timing < (i == timings.Count - 1 ? music.Clip.length : timings[i + 1]))
            {
                _beatTimingsInSong[music.SongName].Add(timing);
                timing += unitTime;
            }
        }
    }

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        Managers.Instance.Game.PlayTime = 0;
        Managers.Instance.Game.SongCount = 0;
        StartCoroutine(MusicPlaying());
    }

    private IEnumerator MusicPlaying()
    {
        Music music = MusicList[Random.Range(0, MusicList.Count)];
        Managers.Instance.Game.PlayingMusic = music;
        PlayingMusic = music;
        beatCounter = 0;
        bpmCounter = 0;

        _audioSource.clip = PlayingMusic.Clip;
        _audioSource.Play();

        Managers.Instance.Game.SongCount++;

        PlayMusic?.Invoke(PlayingMusic);

        yield return new WaitUntil(() => _audioSource.isPlaying == false);

        Managers.Instance.Pool.PopObject(PoolType.CircleArc, Vector3.zero);
    }

    private void Update()
    {
        if(_audioSource.isPlaying)
        {
            Managers.Instance.Game.PlayTime += Time.deltaTime;
            if(beatCounter < _beatTimingsInSong[PlayingMusic.SongName].Count)
            {
                if (_beatTimingsInSong[PlayingMusic.SongName][beatCounter] < _audioSource.time)
                {
                    Managers.Instance.Game.BeatEvent?.Invoke();
                    beatCounter++;
                }
            }

            if(bpmCounter < _bpmTimingsInSong[PlayingMusic.SongName].Count)
            {
                if (_bpmTimingsInSong[PlayingMusic.SongName][bpmCounter] < _audioSource.time)
                {
                    Managers.Instance.Game.SetBPM(PlayingMusic.BpmChangeDict[_bpmTimingsInSong[PlayingMusic.SongName][bpmCounter]]);
                    bpmCounter++;
                }
            }
        }
    }
}
