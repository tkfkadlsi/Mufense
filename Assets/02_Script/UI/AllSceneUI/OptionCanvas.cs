using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : BaseUI
{
    

    private enum ESliders
    {
        MasterVolumeSlider,
        MusicVolumeSlider,
        EffectVolumeSlider
    }

    private enum EButtons
    {
        AutoStartSongButton,
        LowDetailModButton
    }

    private enum ETexts
    {
        AutoStartSongCheckText,
        LowDetailModCheckText,
    }

    private Slider _masterVolumeSlider;
    private Slider _musicVolumeSlider;
    private Slider _effectVolumeSlider;

    private Button _autoStartSongButton;
    private Button _lowDetailModButton;

    private TextMeshProUGUI _autoStartSongCheckText;
    private TextMeshProUGUI _lowDetailModCheckText;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Slider>(typeof(ESliders));
        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));

        _masterVolumeSlider = Get<Slider>((int)ESliders.MasterVolumeSlider);
        _musicVolumeSlider = Get<Slider>((int)ESliders.MusicVolumeSlider);
        _effectVolumeSlider = Get<Slider>((int)ESliders.EffectVolumeSlider);

        _autoStartSongButton = Get<Button>((int)EButtons.AutoStartSongButton);
        _lowDetailModButton = Get<Button>((int)EButtons.LowDetailModButton);

        _autoStartSongCheckText = Get<TextMeshProUGUI>((int)ETexts.AutoStartSongCheckText);
        _lowDetailModCheckText = Get<TextMeshProUGUI>((int)ETexts.LowDetailModCheckText);

        _masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeSlider);
        _musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeSlider);
        _effectVolumeSlider.onValueChanged.AddListener(HandleEffectVolumeSlider);

        _autoStartSongButton.onClick.AddListener(HandleAutoStartSong);
        _lowDetailModButton.onClick.AddListener(HandleLowDetailMod);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        _masterVolumeSlider.value = Managers.Instance.Game.MasterVolume;
        _musicVolumeSlider.value = Managers.Instance.Game.MusicVolume;
        _effectVolumeSlider.value = Managers.Instance.Game.EffectVolume;
    }

    #region Sound

    private void HandleMasterVolumeSlider(float value)
    {
        Managers.Instance.Game.MasterVolume = value;
        Managers.Instance.Game.AudioMixer.SetFloat("Master", value);
    }

    private void HandleMusicVolumeSlider(float value)
    {
        Managers.Instance.Game.MusicVolume = value;
        Managers.Instance.Game.AudioMixer.SetFloat("Music", value);
    }

    private void HandleEffectVolumeSlider(float value)
    {
        Managers.Instance.Game.EffectVolume = value;
        Managers.Instance.Game.AudioMixer.SetFloat("Effect", value);
    }

    #endregion

    #region Game

    private void HandleAutoStartSong()
    {
        Managers.Instance.Game.AutoStartSong = !Managers.Instance.Game.AutoStartSong;

        if(Managers.Instance.Game.AutoStartSong)
        {
            _autoStartSongCheckText.text = "V";
        }
        else
        {
            _autoStartSongCheckText.text = "";
        }
    }

    private void HandleLowDetailMod()
    {
        Managers.Instance.Game.LowDetailMod = !Managers.Instance.Game.LowDetailMod;

        if(Managers.Instance.Game.LowDetailMod)
        {
            _lowDetailModCheckText.text = "V";
        }
        else
        {
            _lowDetailModCheckText.text = "";
        }
    }

    #endregion
}
