using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Music
{
    public string SongName;
    public string ArtistName;
    public AudioClip Clip;
    public TextAsset TowerChaebo;
    public TextAsset EnemyChaebo;
    [SerializedDictionary("Timing", "BPM")] public SerializedDictionary<float, float> BpmChangeDict;
    public List<float> Session = new List<float>();
    public Color PlayerColor;
    public Color EnemyColor;
    public Color BackGroundColor;
    public Color TextColor;
    public int PianoAmount;
    public int DrumAmount;
    public int StringAmount;
    public int CoreAmount;

    private List<float> _beatTimings = new List<float>();
    private List<float> _bpmTimings = new List<float>();
    private List<TowerNote> _towerNoteTimings = new List<TowerNote>();
    private List<EnemyNote> _enemyNoteTimings = new List<EnemyNote>();
    private Dictionary<int, float> _sessionTimings = new Dictionary<int, float>();
}
