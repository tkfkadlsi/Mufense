using DG.Tweening;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : BaseUI, IMusicPlayHandle
{
    private float _songChangeCooltime;

    private enum EButtons
    {
        TowerBuildButton,
        SongChangeButton
    }

    private enum ETexts
    {
        TowerBuildDescription,
        SongChangeDescription
    }

    private Button _towerBuildButton;
    private Button _songChangeButton;

    private TextMeshProUGUI _towerBuildDesc;
    private TextMeshProUGUI _songChangeDesc;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));

        _towerBuildButton = Get<Button>((int)EButtons.TowerBuildButton);
        _songChangeButton = Get<Button>((int)EButtons.SongChangeButton);

        _towerBuildDesc = Get<TextMeshProUGUI>((int)ETexts.TowerBuildDescription);
        _songChangeDesc = Get<TextMeshProUGUI>((int)ETexts.SongChangeDescription);

        _towerBuildButton.onClick.AddListener(HandleTowerBuildButton);
        _songChangeButton.onClick.AddListener(HandleSongChangeButton);

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return true;
    }

    private void HandleTowerBuildButton()
    {
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("BuildCanvas", true);
    }

    private void HandleSongChangeButton()
    {
        if (_songChangeCooltime < 10f) return;
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("SongCanvas", true);
    }

    public void SetBuildButtonActive(bool active)
    {
        _towerBuildButton.gameObject.SetActive(active);
    }

    public void SettingColor(Music music)
    {
        _songChangeCooltime = 0f;
        _towerBuildButton.image.DOColor(music.PlayerColor, 1f);
        _songChangeButton.image.color = new Color(music.PlayerColor.r, music.PlayerColor.g, music.PlayerColor.b, 0f);
        _towerBuildDesc.DOColor(music.TextColor, 1f);
        _songChangeDesc.DOColor(music.TextColor, 1f);
    }

    private void Update()
    {
        _songChangeCooltime += Time.deltaTime;

        if (_songChangeCooltime > 10f)
        {
            _songChangeCooltime = 10f;
        }

        _songChangeButton.image.color = new Color(
            _songChangeButton.image.color.r,
            _songChangeButton.image.color.g,
            _songChangeButton.image.color.b,
            _songChangeCooltime / 10f);
    }
}
