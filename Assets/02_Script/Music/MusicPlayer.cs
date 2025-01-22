using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : BaseInit
{
    public List<Music> MusicList = new List<Music>();

    private AudioSource _audioSource;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _audioSource = GetComponent<AudioSource>();

        return true;
    }

    public IEnumerator MusicPlaying()
    {
        Music music = MusicList[Random.Range(0, MusicList.Count)];
        Managers.Instance.Game.PlayingMuisc = music;

        _audioSource.clip = music.Clip;
        _audioSource.Play();

        Managers.Instance.Game.SongCount++;

        yield return new WaitUntil(() => _audioSource.isPlaying == false);

        Managers.Instance.Game.SongFinish();
    }


}
