using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPowerChest : BaseInit, IMusicHandleObject
{
    public int MaxMusicPower { get; private set; }
    private int _musicPower;

    [SerializeField] private Image _backGround;
    [SerializeField] private Image _fill;
    [SerializeField] private Image _subFill;

    private Slider _musicPowerSlider;
    private TextMeshProUGUI _musicPowerCounter;
    private int _cooldown;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _musicPower = 100;
        _musicPowerCounter = gameObject.FindChild<TextMeshProUGUI>("");
        _musicPowerSlider = GetComponent<Slider>();
        _musicPowerSlider.value = _musicPower;
        _subFill.fillAmount = 0;

        SetMaxMusicPower(500);

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += HandlePlayMusic;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent += HandleMusicBeat;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            if (Managers.Instance.Game.FindBaseInitScript<MusicPlayer>() != null)
            {
                Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= HandlePlayMusic;
                Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().BeatEvent -= HandleMusicBeat;
            }
        }

        base.Release();
    }

    private void HandlePlayMusic(Music music)
    {
        _fill.color = music.EnemyColor;
        _backGround.color = _fill.color * 0.5f;
        _musicPowerCounter.color = new Color(255 - music.EnemyColor.r, 255 - music.EnemyColor.g, 255 - music.EnemyColor.b);
        _subFill.color = new Color(music.EnemyColor.r, music.EnemyColor.g, music.EnemyColor.b, 0.5f);
    }

    public void SetMaxMusicPower(int value)
    {
        MaxMusicPower = value;
        SetCounter(true);
    }

    public void AddMusicPower(int value)
    {
        Debug.Log($"[사람이냐] : 추가 뮤직 파워 : {value}");
        _musicPower += value;

        if (_musicPower > MaxMusicPower)
        {
            _musicPower = MaxMusicPower;
        }

        SetCounter(false);
    }

    public bool CanRemoveMusicPower(int value)
    {
        return value <= _musicPower;
    }

    public bool RemoveMusicPower(int value)
    {
        if (_musicPower >= value)
        {
            _musicPower -= value;
            SetCounter(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetCounter(bool isRemove)
    {
        if(isRemove)
        {
            _subFill.fillAmount = (float)_musicPower / MaxMusicPower;
            _musicPowerSlider.maxValue = MaxMusicPower;
            _musicPowerSlider.value = _musicPower;
            _musicPowerCounter.text = $"({_musicPower} / {MaxMusicPower})";
        }
        else
        {
            _subFill.fillAmount = (float)_musicPower / MaxMusicPower;
            _cooldown = 8;
            _musicPowerSlider.maxValue = MaxMusicPower;
            _musicPowerCounter.text = $"({_musicPower} / {MaxMusicPower})";
        }
    }

    public void HandleMusicBeat()
    {
        _cooldown--;
        if(_cooldown == 0)
        {
            _musicPowerSlider.DOValue(_musicPower, Managers.Instance.Game.UnitTime * 4);
        }
    }
}
