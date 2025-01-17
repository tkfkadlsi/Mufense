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

    private Slider _masterVolumeSlider;
    private Slider _musicVolumeSlider;
    private Slider _effectVolumeSlider;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Slider>(typeof(ESliders));

        _masterVolumeSlider = Get<Slider>((int)ESliders.MasterVolumeSlider);
        _musicVolumeSlider = Get<Slider>((int)ESliders.MusicVolumeSlider);
        _effectVolumeSlider = Get<Slider>((int)ESliders.EffectVolumeSlider);

        _masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeSlider);
        _musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeSlider);
        _effectVolumeSlider.onValueChanged.AddListener(HandleEffectVolumeSlider);

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
}
