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
    public Color PlayerColor;
    public Color EnemyColor;
    public Color PlayerAttackColor;
    public Color CoreColor;
    public Color WallColor;
    public Color BackGroundColor;
}
