using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

[Serializable]
public class Music
{
    public string SongName;
    public string ArtistName;
    public AudioClip Clip;
    [SerializedDictionary("Timing", "BPM")] public SerializedDictionary<float, float> BpmChangeDict;
    public TextAsset LineSheet;
    public TextAsset BombSheet;
    public TextAsset BoomSheet;
    public TextAsset ArcSheet;
}
