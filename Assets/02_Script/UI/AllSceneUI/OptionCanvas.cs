using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class OptionCanvas : BaseUI
{
    private enum EGameObjects
    {
        SoundSetting,
        ControlSetting,
        GameSetting
    }

    private enum EOutlines
    {
        SelectSound,
        SelectControl,
        SelectGamePlay
    }

    private enum ESliders
    {
        MasterVolumeSlider,
        MusicVolumeSlider,
        EffectVolumeSlider
    }

    private enum EButtons
    {
        SelectSound,
        SelectControl,
        SelectGamePlay,
        AutoStartSongButton,
        LowDetailModButton,
        ChangeLanguageButton,
        ExitButton
    }

    private enum ETexts
    {
        AutoStartSongCheckText,
        LowDetailModCheckText
    }

    private GameObject _soundSetting;
    private GameObject _controlSetting;
    private GameObject _gameSetting;

    private Outline _selectSoundOutline;
    private Outline _selectControlOutline;
    private Outline _selectGamePlayOutline;

    private Slider _masterVolumeSlider;
    private Slider _musicVolumeSlider;
    private Slider _effectVolumeSlider;

    private Button _selectSound;
    private Button _selectControl;
    private Button _selectGamePlay;

    private Button _autoStartSongButton;
    private Button _lowDetailModButton;
    private Button _changeLanguageButton;

    private Button _exitButton;

    private TextMeshProUGUI _autoStartSongCheckText;
    private TextMeshProUGUI _lowDetailModCheckText;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<GameObject>(typeof(EGameObjects));
        Bind<Outline>(typeof(EOutlines));
        Bind<Slider>(typeof(ESliders));
        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));

        _soundSetting = Get<GameObject>((int)EGameObjects.SoundSetting);
        _controlSetting = Get<GameObject>((int)EGameObjects.ControlSetting);
        _gameSetting = Get<GameObject>((int)EGameObjects.GameSetting);

        _selectSoundOutline = Get<Outline>((int)EOutlines.SelectSound);
        _selectControlOutline = Get<Outline>((int)EOutlines.SelectControl);
        _selectGamePlayOutline = Get<Outline>((int)EOutlines.SelectGamePlay);

        _masterVolumeSlider = Get<Slider>((int)ESliders.MasterVolumeSlider);
        _musicVolumeSlider = Get<Slider>((int)ESliders.MusicVolumeSlider);
        _effectVolumeSlider = Get<Slider>((int)ESliders.EffectVolumeSlider);

        _selectSound = Get<Button>((int)EButtons.SelectSound);
        _selectControl = Get<Button>((int)EButtons.SelectControl);
        _selectGamePlay = Get<Button>((int)EButtons.SelectGamePlay);

        _autoStartSongButton = Get<Button>((int)EButtons.AutoStartSongButton);
        _lowDetailModButton = Get<Button>((int)EButtons.LowDetailModButton);
        _changeLanguageButton = Get<Button>((int)EButtons.ChangeLanguageButton);

        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _autoStartSongCheckText = Get<TextMeshProUGUI>((int)ETexts.AutoStartSongCheckText);
        _lowDetailModCheckText = Get<TextMeshProUGUI>((int)ETexts.LowDetailModCheckText);

        _masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeSlider);
        _musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeSlider);
        _effectVolumeSlider.onValueChanged.AddListener(HandleEffectVolumeSlider);

        _selectSound.onClick.AddListener(HandleSelectSound);
        _selectControl.onClick.AddListener(HandleSelectControl);
        _selectGamePlay.onClick.AddListener(HandleSelectGamePlay);

        _autoStartSongButton.onClick.AddListener(HandleAutoStartSong);
        _lowDetailModButton.onClick.AddListener(HandleLowDetailMod);
        _changeLanguageButton.onClick.AddListener(HandleChangeLanguage);

        _exitButton.onClick.AddListener(HandleExit);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        HandleSelectSound();

        _masterVolumeSlider.value = Managers.Instance.Game.MasterVolume;
        _musicVolumeSlider.value = Managers.Instance.Game.MusicVolume;
        _effectVolumeSlider.value = Managers.Instance.Game.EffectVolume;

        _autoStartSongCheckText.text = Managers.Instance.Game.AutoStartSong ? "V" : "";
        _lowDetailModCheckText.text = Managers.Instance.Game.LowDetailMod ? "V" : "";
    }

    private void HandleExit()
    {
        Managers.Instance.UI.TitleRootUI.SetActiveCanvas("OptionCanvas", false);
    }

    #region Select

    private void HandleSelectSound()
    {
        ChangeOutlineColor(EOutlines.SelectSound);
        ChangeActiveOption(EGameObjects.SoundSetting);
    }

    private void HandleSelectControl()
    {
        ChangeOutlineColor(EOutlines.SelectControl);
        ChangeActiveOption(EGameObjects.ControlSetting);
    }

    private void HandleSelectGamePlay()
    {
        ChangeOutlineColor(EOutlines.SelectGamePlay);
        ChangeActiveOption(EGameObjects.GameSetting);
    }

    private void ChangeOutlineColor(EOutlines Eoutline)
    {
        Color yellow = new Color(1, 0.76f, 0);
        Color gray = new Color(0.66f, 0.66f, 0.66f);

        _selectSoundOutline.effectColor = gray;
        _selectControlOutline.effectColor = gray;
        _selectGamePlayOutline.effectColor = gray;

        switch(Eoutline)
        {
            case EOutlines.SelectSound:
                _selectSoundOutline.effectColor = yellow;
                break;
            case EOutlines.SelectControl:
                _selectControlOutline.effectColor = yellow;
                break;
            case EOutlines.SelectGamePlay:
                _selectGamePlayOutline.effectColor = yellow;
                break;
        }
    }

    private void ChangeActiveOption(EGameObjects Ego)
    {
        _soundSetting.SetActive(false);
        _controlSetting.SetActive(false);
        _gameSetting.SetActive(false);

        switch(Ego)
        {
            case EGameObjects.SoundSetting:
                _soundSetting.SetActive(true);
                break;
            case EGameObjects.ControlSetting:
                _controlSetting.SetActive(true);
                break;
            case EGameObjects.GameSetting:
                _gameSetting.SetActive(true);
                break;
        }
    }

    #endregion

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

    private void HandleChangeLanguage()
    {
        switch(Managers.Instance.Game.Language)
        {
            case Language.English:
                Managers.Instance.Game.Language = Language.Korean;
                break;

            case Language.Korean:
                Managers.Instance.Game.Language = Language.English;
                break;
        }

        Managers.Instance.Game.ChangeLanguageEvent(Managers.Instance.Game.Language);
    }

    #endregion
}
