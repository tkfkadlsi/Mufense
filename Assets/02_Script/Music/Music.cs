using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Music
{
    public string SongName;
    public string ArtistName;
    public AudioClip Clip;
    public TextAsset Chaebo;
    [SerializedDictionary("Timing", "BPM")] public SerializedDictionary<float, float> BpmChangeDict;
    public Color PlayerColor;
    public Color EnemyColor;
    public Color BackGroundColor;
    public Color TextColor;
    public List<float> CircleArcAttackTimings = new List<float>();
    public int PianoAmount;
    public int DrumAmount;
    public int StringAmount;
    public int CoreAmount;
}
