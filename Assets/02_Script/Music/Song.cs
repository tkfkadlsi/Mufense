using System;
using UnityEngine;

[Serializable]
public class Song
{
    public AudioClip Music;
    public float MinBPM;
    public float MaxBPM;
    public TextAsset SheetMusic;
    public string SongName;
    public string ArtistName;
}
