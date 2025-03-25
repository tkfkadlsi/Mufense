using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPowerChest : BaseInit
{
    public int MaxMusicPower { get; private set; }
    private int _musicPower;

    [SerializeField] private Image _backGround;
    [SerializeField] private Image _fill;

    private Slider _musicPowerSlider;
    private TextMeshProUGUI _musicPowerCounter;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _musicPowerCounter = gameObject.FindChild<TextMeshProUGUI>("");
        _musicPowerSlider = GetComponent<Slider>();

        _musicPower = 300;

        SetMaxMusicPower(500);

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += HandlePlayMusic;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            if (Managers.Instance.Game.FindBaseInitScript<MusicPlayer>() != null)
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
        MaxMusicPower = value;
        SetCounter();
    }

    public void AddMusicPower(int value)
    {
        Debug.Log($"[사람이냐] : 추가 뮤직 파워 : {value}");
        _musicPower += value;

        if (_musicPower > MaxMusicPower)
        {
            _musicPower = MaxMusicPower;
        }

        SetCounter();
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
        _musicPowerSlider.value = _musicPower;
        _musicPowerSlider.maxValue = MaxMusicPower;
        _musicPowerCounter.text = $"({_musicPower} / {MaxMusicPower})";
    }
}
