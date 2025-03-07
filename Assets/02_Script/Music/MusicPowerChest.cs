using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MusicPowerChest : BaseInit
{
    [SerializeField] private Image _backGround;
    [SerializeField] private Image _fill;

    private Slider _musicPowerSlider;
    private TextMeshProUGUI _musicPowerCounter;

    private int _maxMusicPower;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _musicPowerCounter = gameObject.FindChild<TextMeshProUGUI>("");
        _musicPowerSlider = GetComponent<Slider>();

        _maxMusicPower = 300;
        _musicPowerSlider.value = 300;

        SetMaxMusicPower(1000);

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += HandlePlayMusic;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            if(Managers.Instance.Game.FindBaseInitScript<MusicPlayer>() != null)
            {
                Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= HandlePlayMusic;
            }
        }

        base.Release();
    }

    private void HandlePlayMusic(Music music)
    {
        _fill.color = music.EnemyColor;
        _backGround.color = _fill.color * 0.5f;
        _musicPowerCounter.color = new Color(255 - music.EnemyColor.r, 255 - music.EnemyColor.g, 255 - music.EnemyColor.b);
    }

    public void SetMaxMusicPower(int value)
    {
        _maxMusicPower = value;
        SetCounter();
    }

    public void AddMusicPower(int value)
    {
        _musicPowerSlider.value += value;
        SetCounter();
    }

    public bool CanRemoveMusicPower(int value)
    {
        return value >= _musicPowerSlider.value;
    }

    public bool RemoveMusicPower(int value)
    {
        if (_musicPowerSlider.value >= value)
        {
            _musicPowerSlider.value -= value;
            SetCounter();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetCounter()
    {
        _musicPowerCounter.text = $"({_musicPowerSlider.value} / {_maxMusicPower})";
    }
}
