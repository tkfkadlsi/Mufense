using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlayer : BaseInit
{
    public List<Music> MusicList = new List<Music>();

    public event Action<Music> PlayMusic;
    public event Action BeatEvent;
    public event Action<TowerType> NoteEvent;

    private AudioSource _audioSource;
    private int beatCounter = 0;
    private int noteCounter = 0;
    private int bpmCounter = 0;

    public Music PlayingMusic { get; private set; } = null;
    public float MusicTime => _audioSource.time;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _audioSource = GetComponent<AudioSource>();

        foreach (Music music in MusicList)
        {
            music.MakeBeat();
        }

        return true;
    }

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        Managers.Instance.Game.PlayTime = 0;
        StartCoroutine(MusicPlaying());
    }

    private IEnumerator MusicPlaying()
    {
        Music music = MusicList[Random.Range(0, MusicList.Count)];
        PlayingMusic = music;
        beatCounter = 0;
        noteCounter = 0;
        bpmCounter = 0;

        _audioSource.clip = PlayingMusic.Clip;
        _audioSource.Play();

        PlayMusic?.Invoke(PlayingMusic);

        yield return new WaitUntil(() => _audioSource.isPlaying == false);

        StartCoroutine(MusicPlaying());
    }

    public async Awaitable ChangeMusic(Music music)
    {
        float time = _audioSource.time;

        await Awaitable.BackgroundThreadAsync();

        int beatCounter = 0;
        int noteCounter = 0;
        int bpmCounter = 0;

        while (time > music.GetBeatTiming(beatCounter))
        {
            beatCounter++;
            if (beatCounter >= music.GetBeatTimingCount()) break;
        }

        while (time > music.GetTowerNote(noteCounter).timing)
        {
            noteCounter++;
            if (noteCounter >= music.GetTowerNoteCount()) break;
        }

        while (time > music.GetBpmChangeTiming(bpmCounter))
        {
            bpmCounter++;
            if (bpmCounter >= music.GetBpmChangeTimingCount()) break;
        }

        await Awaitable.MainThreadAsync();


        this.beatCounter = beatCounter;
        this.noteCounter = noteCounter;
        this.bpmCounter = bpmCounter;

        PlayingMusic = music;

        _audioSource.clip = music.Clip;
        _audioSource.time = time;
        _audioSource.Play();

        PlayMusic?.Invoke(PlayingMusic);
    }

    public void SetPitch(float pitch)
    {
        _audioSource.pitch = pitch;
    }

    public void SetMute(bool mute)
    {
        _audioSource.mute = mute;
    }

    private void Update()
    {
        if (_audioSource.isPlaying)
        {
            Managers.Instance.Game.PlayTime += Time.deltaTime;

            if (beatCounter < PlayingMusic.GetBeatTimingCount())
            {
                if (PlayingMusic.GetBeatTiming(beatCounter) < _audioSource.time)
                {
                    BeatEvent?.Invoke();
                    beatCounter++;
                }
            }

            if (noteCounter < PlayingMusic.GetTowerNoteCount())
            {
                while (PlayingMusic.GetTowerNote(noteCounter).timing < _audioSource.time)
                {
                    NoteEvent?.Invoke(PlayingMusic.GetTowerNote(noteCounter).type);
                    noteCounter++;

                    if (noteCounter >= PlayingMusic.GetTowerNoteCount())
                    {
                        break;
                    }
                }
            }

            if (bpmCounter < PlayingMusic.GetBpmChangeTimingCount())
            {
                if (PlayingMusic.GetBpmChangeTiming(bpmCounter) < _audioSource.time)
                {
                    Managers.Instance.Game.SetBPM(PlayingMusic.GetBpmChangeTiming(bpmCounter));
                    bpmCounter++;
                }
            }
        }
    }
}
