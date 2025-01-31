using TMPro;
using UnityEngine.UI;

public class MusicPowerChest : BaseInit
{
    private Slider _musicPowerSlider;
    private TextMeshProUGUI _musicPowerCounter;

    private int _maxMusicPower;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _musicPowerSlider = gameObject.FindChild<Slider>("");
        _musicPowerCounter = gameObject.FindChild<TextMeshProUGUI>("");

        _maxMusicPower = 0;
        _musicPowerSlider.value = 0;

        return true;
    }

    public void SetMaxMusicPower(int value)
    {
        _maxMusicPower = value;
    }

    public void AddMusicPower(int value)
    {
        _musicPowerSlider.value += value;
        SetCounter();
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
