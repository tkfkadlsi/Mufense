using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlayer : BaseInit
{
    public List<Music> MusicList = new List<Music>();
    public List<Music> PlayableMusicList = new List<Music>();

    public event Action<Music> PlayMusic;
    public event Action BeatEvent;
    public event Action<TowerType> NoteEvent;

    private Dictionary<string, List<float>> _beatTimingsInSong = new Dictionary<string, List<float>>();
    private Dictionary<string, List<Note>> _noteTimingsInSong = new Dictionary<string, List<Note>>();
    private Dictionary<string, List<float>> _bpmTimingsInSong = new Dictionary<string, List<float>>();
    private Dictionary<string, List<float>> _circleArcAttackTimingsInSong = new Dictionary<string, List<float>>();
    private AudioSource _audioSource;
    private int beatCounter = 0;
    private int noteCounter = 0;
    private int bpmCounter = 0;
    private int attackCounter = 0;
    public Music PlayingMusic { get; private set; }

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
            _circleArcAttackTimingsInSong.Add(music.SongName, new List<float>());
            _noteTimingsInSong.Add(music.SongName, new List<Note>());
            MakeBeatTiming(music);
        }

        for(int i = 0; i < 2; i++)
        {
            Music playableMusic = MusicList[Random.Range(0, MusicList.Count)];
            PlayableMusicList.Add(playableMusic);
            MusicList.Remove(playableMusic);
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

        foreach (float t in music.CircleArcAttackTimings)
        {
            _circleArcAttackTimingsInSong[music.SongName].Add(t);
        }

        string[] notes = music.Chaebo.ToString().Split('\n');
        foreach (var note in notes)
        {
            string[] noteinfos = note.Split(',');

            Note newNote = new Note();
            newNote.type = ParseChaeboToTowerType(noteinfos[0]);
            newNote.timing = int.Parse(noteinfos[2]) / 1000f;

            _noteTimingsInSong[music.SongName].Add(newNote);
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

    private TowerType ParseChaeboToTowerType(string chaebotxt)
    {
        switch(chaebotxt)
        {
            case "36":
                return TowerType.Piano;
            case "109":
                return TowerType.Drum;
            case "182":
                return TowerType.String;
            case "256":
            case "329":
            case "402":
            case "475":
                return TowerType.Core;
            default:
                return TowerType.None;
        }
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
        Music music = PlayableMusicList[Random.Range(0, PlayableMusicList.Count)];
        //MusicList.Remove(music);
        PlayingMusic = music;
        beatCounter = 0;
        noteCounter = 0;
        bpmCounter = 0;
        attackCounter = 0;

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
        int attackCounter = 0;

        while(time > _beatTimingsInSong[music.SongName][beatCounter])
        {
            beatCounter++;
            if (beatCounter >= _beatTimingsInSong[music.SongName].Count) break;
        }

        while(time > _noteTimingsInSong[music.SongName][noteCounter].timing)
        {
            noteCounter++;
            if (noteCounter >= _noteTimingsInSong[music.SongName].Count) break;
        }

        while(time > _bpmTimingsInSong[music.SongName][bpmCounter])
        {
            bpmCounter++;
            if (bpmCounter >= _bpmTimingsInSong[music.SongName].Count) break;
        }

        while(time > _circleArcAttackTimingsInSong[music.SongName][attackCounter])
        {
            attackCounter++;
            if (attackCounter >= _circleArcAttackTimingsInSong[music.SongName].Count) break;
        }

        await Awaitable.MainThreadAsync();

        this.beatCounter = beatCounter;
        this.noteCounter = noteCounter;
        this.bpmCounter = bpmCounter;
        this.attackCounter = attackCounter;

        PlayingMusic = music;

        _audioSource.clip = music.Clip;
        _audioSource.time = time;
        _audioSource.Play();

        PlayMusic?.Invoke(PlayingMusic);
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
                    BeatEvent?.Invoke();
                    beatCounter++;
                }
            }

            if(noteCounter < _noteTimingsInSong[PlayingMusic.SongName].Count)
            {
                while (_noteTimingsInSong[PlayingMusic.SongName][noteCounter].timing < _audioSource.time)
                {
                    NoteEvent?.Invoke(_noteTimingsInSong[PlayingMusic.SongName][noteCounter].type);
                    noteCounter++;

                    if(noteCounter >= _noteTimingsInSong[PlayingMusic.SongName].Count)
                    {
                        break;
                    }
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

            if(attackCounter < _circleArcAttackTimingsInSong[PlayingMusic.SongName].Count)
            {
                if (_circleArcAttackTimingsInSong[PlayingMusic.SongName][attackCounter] < _audioSource.time)
                {
                    Managers.Instance.Game.FindBaseInitScript<Core>().CircleArcAttack();
                    attackCounter++;
                }
            }
        }
    }
}
