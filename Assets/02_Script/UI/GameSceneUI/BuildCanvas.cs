using DG.Tweening;
using System.Collections;
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
        ExitButton
    }

    enum EImages
    {
        BuildingsPanel
    }

    private Button _pianoTower;
    private Button _exitButton;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));
        Bind<Image>(typeof(EImages));

        _pianoTower = Get<Button>((int)EButtons.PianoTower);
        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _pianoTower.onClick.AddListener(HandlePianoTower);
        _exitButton.onClick.AddListener(HandleExitButton);

        PianoCost = 100;
        DrumCost = 100;
        StringCost = 100;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
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
        _pianoTower.image.DOColor(music.PlayerColor, 1f);
        
    }
}