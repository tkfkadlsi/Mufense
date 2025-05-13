using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Music
{
    private static TowerType ParseChaeboToTowerType(string chaebotxt)
    {
        switch (chaebotxt)
        {
            case "36":
                return TowerType.Piano;
            case "109":
                return TowerType.Drum;
            case "182":
                return TowerType.String;
            case "256":
                return TowerType.CoreRotate;
            case "329":
                return TowerType.CoreRotate2;
            case "402":
                return TowerType.CoreRotate4;
            case "475":
                return TowerType.CoreAttack;
            default:
                return TowerType.None;
        }
    }

    public string SongName;
    public string ArtistName;
    public AudioClip Clip;
    public TextAsset TowerChaebo;
    [SerializedDictionary("Timing", "BPM")] public SerializedDictionary<float, float> BpmChangeDict;
    public int BeatInBar;
    public Color PlayerColor;
    public Color EnemyColor;
    public Color BackGroundColor;
    public Color TextColor;
    public Color IconColor;
    public Color PianoAttackColor;
    public Color DrumAttackColor;
    public Color StringAttackColor;
    public int PianoAmount;
    public int DrumAmount;
    public int StringAmount;
    public int CoreAmount;

    private List<float> _beatTimings = new List<float>();
    private List<float> _bpmChangeTimings = new List<float>();
    private List<TowerNote> _towerNoteTimings = new List<TowerNote>();

    public void MakeBeat()
    {
        List<float> timings = new List<float>();
        foreach (var bcd in BpmChangeDict)
        {
            timings.Add(bcd.Key);
            _bpmChangeTimings.Add(bcd.Key);
        }

        string[] notes = TowerChaebo.ToString().Split('\n');
        foreach (var note in notes)
        {
            string[] noteinfos = note.Split(',');

            TowerNote newNote = new TowerNote();
            newNote.type = ParseChaeboToTowerType(noteinfos[0]);
            newNote.timing = int.Parse(noteinfos[2]) / 1000f;

            _towerNoteTimings.Add(newNote);
        }

        float timing = 0f;
        float unitTime = 0f;

        for (int i = 0; i < timings.Count; i++)
        {
            timing = timings[i];
            unitTime = 60f / BpmChangeDict[timings[i]];

            while (timing < (i == timings.Count - 1 ? Clip.length : timings[i + 1]))
            {
                _beatTimings.Add(timing);
                timing += unitTime;
            }
        }
    }

    public float GetBeatTiming(int count)
    {
        return _beatTimings[count];
    }
    public int GetBeatTimingCount()
    {
        return _beatTimings.Count;
    }

    public float GetBpmChangeTiming(int count)
    {
        return _bpmChangeTimings[count];
    }
    public int GetBpmChangeTimingCount()
    {
        return _bpmChangeTimings.Count;
    }
    public float GetBPM(int count)
    {
        return BpmChangeDict[GetBeatTiming(count)];
    }

    public TowerNote GetTowerNote(int count)
    {
        return _towerNoteTimings[count];
    }
    public int GetTowerNoteCount()
    {
        return _towerNoteTimings.Count;
    }
}
