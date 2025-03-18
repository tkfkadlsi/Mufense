using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SongCanvas : BaseUI
{
    private IEnumerator _songPanelCoroutine;
    private List<SongButton> _songButtonList = new List<SongButton>();

    enum EImages
    {
        Panel,
        SongPanel,
    }

    enum ETexts
    {
        SongName,
        ArtistName,
        BPM
    }

    enum ESliders
    {
        PianoSlider,
        DrumSlider,
        StringSlider,
        CoreSlider
    }

    enum EButtons
    {
        ExitButton
    }

    private Image _panel;
    private Image _songPanel;

    private TextMeshProUGUI _songName;
    private TextMeshProUGUI _artistName;
    private TextMeshProUGUI _bpm;

    private Slider _pianoSlider;
    private Slider _drumSlider;
    private Slider _stringSlider;
    private Slider _coreSlider;

    private Button _exitButton;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Image>(typeof(EImages));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Slider>(typeof(ESliders));
        Bind<Button>(typeof(EButtons));

        _panel = Get<Image>((int)EImages.Panel);
        _songPanel = Get<Image>((int)EImages.SongPanel);

        _songName = Get<TextMeshProUGUI>((int)ETexts.SongName);
        _artistName = Get<TextMeshProUGUI>((int)ETexts.ArtistName);
        _bpm = Get<TextMeshProUGUI>((int)ETexts.BPM);

        _pianoSlider = Get<Slider>((int)ESliders.PianoSlider);
        _drumSlider = Get<Slider>((int)ESliders.DrumSlider);
        _stringSlider = Get<Slider>((int)ESliders.StringSlider);
        _coreSlider = Get<Slider>((int)ESliders.CoreSlider);

        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _exitButton.onClick.AddListener(HandleExitButton);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        _songPanel.rectTransform.localScale = Vector3.one * 0.0001f;

        foreach(Music music in Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayableMusicList)
        {
            if (music == Managers.Instance.Game.PlayingMusic) continue;
            if(music.Clip.length < Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().MusicTime) continue;

            SongButton songButton = Managers.Instance.Pool.PopObject(PoolType.SongButton, Vector3.zero, _panel.transform).GetComponent<SongButton>();
            songButton.transform.localScale = Vector3.zero;
            songButton.transform.DOScale(1f, 0.25f);
            songButton.SettingButton(music);
            _songButtonList.Add(songButton);
        }
    }

    protected override void ActiveOff()
    {
        while(_songButtonList.Count > 0)
        {
            _songButtonList[0].PushThisObject();
            _songButtonList.RemoveAt(0);
        }

        base.ActiveOff();
    }

    public void SettingSongPanel(Music music)
    {
        _songName.text = music.SongName;
        _artistName.text = $"Artist : {music.ArtistName}";

        float minbpm = float.MaxValue;
        float maxbpm = float.MinValue;
        foreach(var dict in music.BpmChangeDict)
        {
            if(minbpm > dict.Value) minbpm = dict.Value;
            if(maxbpm < dict.Value) maxbpm = dict.Value;
        }

        if(minbpm == maxbpm)
        {
            _bpm.text = $"Bpm : {minbpm}";
        }
        else
        {
            _bpm.text = $"Bpm : {minbpm}~{maxbpm}";
        }

        _pianoSlider.value = music.PianoAmount;
        _drumSlider.value = music.DrumAmount;
        _stringSlider.value = music.StringAmount;
        _coreSlider.value = music.CoreAmount;

        if(_songPanelCoroutine is not null)
        {
            StopCoroutine(_songPanelCoroutine);
        }
        _songPanelCoroutine = SongPanelOpen();
        StartCoroutine(_songPanelCoroutine);
    }

    public void ReleaseSongPanel()
    {
        if (_songPanelCoroutine is not null)
        {
            StopCoroutine(_songPanelCoroutine);
        }
        _songPanelCoroutine = SongPanelClose();
        StartCoroutine(_songPanelCoroutine);
    }

    private IEnumerator SongPanelOpen()
    {
        float t = 0f;
        float lerpTime = 0.5f;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _songPanel.rectTransform.localScale = Vector3.Lerp(_songPanel.rectTransform.localScale, Vector3.one, t / lerpTime);
        }
    }

    private IEnumerator SongPanelClose()
    {
        float t = 0f;
        float lerpTime = 0.5f;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _songPanel.rectTransform.localScale = Vector3.Lerp(_songPanel.rectTransform.localScale, Vector3.zero, t / lerpTime);
        }
    }

    private void HandleExitButton()
    {
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("SongCanvas", false);
    }
}
