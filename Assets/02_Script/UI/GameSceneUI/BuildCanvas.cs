using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildCanvas : BaseUI, IMusicPlayHandle
{
    public int PianoCost;
    public int DrumCost;
    public int StringCost;

    enum EButtons
    {
        PianoTower,
        DrumTower,
        StringTower,
        ExitButton
    }

    enum EImages
    {
        PianoTower,
        DrumTower,
        StringTower,
        ExitButton
    }
    
    enum ETexts
    {
        PianoCostText,
        DrumCostText,
        StringCostText,
    }

    private Button _pianoTower;
    private Button _drumTower;
    private Button _stringTower;
    private Button _exitButton;

    private Image _pianoImage;
    private Image _drumImage;
    private Image _stringImage;
    private Image _exitImage;

    private TextMeshProUGUI _pianoCostText;
    private TextMeshProUGUI _drumCostText;
    private TextMeshProUGUI _stringCostText;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));
        Bind<Image>(typeof(EImages));
        Bind<TextMeshProUGUI>(typeof(ETexts));

        _pianoTower = Get<Button>((int)EButtons.PianoTower);
        _drumTower = Get<Button>((int)EButtons.DrumTower);
        _stringTower = Get<Button>((int)EButtons.StringTower);
        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _pianoImage = Get<Image>((int)EImages.PianoTower);
        _drumImage = Get<Image>((int)EImages.DrumTower);
        _stringImage = Get<Image>((int)EImages.StringTower);
        _exitImage = Get<Image>((int)EImages.ExitButton);

        _pianoCostText = Get<TextMeshProUGUI>((int)ETexts.PianoCostText);
        _drumCostText = Get<TextMeshProUGUI>((int)ETexts.DrumCostText);
        _stringCostText = Get<TextMeshProUGUI>((int)ETexts.StringCostText);

        _pianoTower.onClick.AddListener(HandlePianoTower);
        _drumTower.onClick.AddListener(HandleDrumTower);
        _stringTower.onClick.AddListener(HandleStringTower);
        _exitButton.onClick.AddListener(HandleExitButton);

        PianoCost = 150;
        DrumCost = 150;
        StringCost = 150;

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        _pianoImage.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        _drumImage.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        _stringImage.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        _exitImage.color = Managers.Instance.Game.PlayingMusic.PlayerColor;

        _pianoCostText.text = PianoCost.ToString();
        _drumCostText.text = DrumCost.ToString();
        _stringCostText.text = StringCost.ToString();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        StartCoroutine(PanelOpen());
    }

    protected override void ActiveOff()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.ActiveOff();
    }

    private void HandlePianoTower()
    {
        if(Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().CanRemoveMusicPower(PianoCost))
        {
            Managers.Instance.Game.FindBaseInitScript<TowerSpawner>().SetSpawnState(TowerSpawnState.Create, TowerType.Piano, PianoCost);
            DisableBuildingButton();
            HandleExitButton();
        }
    }

    private void HandleDrumTower()
    {
        if (Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().CanRemoveMusicPower(DrumCost))
        {
            Managers.Instance.Game.FindBaseInitScript<TowerSpawner>().SetSpawnState(TowerSpawnState.Create, TowerType.Drum, DrumCost);
            DisableBuildingButton();
            HandleExitButton();
        }
    }

    private void HandleStringTower()
    {
        if(Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().CanRemoveMusicPower(StringCost))
        {
            Managers.Instance.Game.FindBaseInitScript<TowerSpawner>().SetSpawnState(TowerSpawnState.Create, TowerType.String, StringCost);
            DisableBuildingButton();
            HandleExitButton();
        }
    }

    private void HandleExitButton()
    {
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("BuildCanvas", false);
    }

    private void DisableBuildingButton()
    {
        Managers.Instance.UI.GameRootUI.MainCanvas.SetBuildButtonActive(false);
    }

    public void SettingColor(Music music)
    {
        _pianoImage.DOColor(music.PlayerColor, 1f);
        _drumImage.DOColor(music.PlayerColor, 1f);
        _stringImage.DOColor(music.PlayerColor, 1f);
        _exitImage.DOColor(music.PlayerColor, 1f);
    }

    private IEnumerator PanelOpen()
    {
        _pianoTower.transform.localScale = Vector3.zero;
        _drumTower.transform.localScale = Vector3.zero;
        _stringTower.transform.localScale = Vector3.zero;
        _exitButton.transform.localScale = Vector3.zero;

        float t = 0f;
        float lerpTime = 0.5f;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            _pianoTower.transform.localScale = Vector3.Lerp(_pianoTower.transform.localScale, Vector3.one, t / lerpTime);
            _drumTower.transform.localScale = Vector3.Lerp(_drumTower.transform.localScale, Vector3.one, t / lerpTime);
            _stringTower.transform.localScale = Vector3.Lerp(_stringTower.transform.localScale, Vector3.one, t / lerpTime);
            _exitButton.transform.localScale = Vector3.Lerp(_exitButton.transform.localScale, Vector3.one, t / lerpTime);
        }
    }
}