using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildCanvas : BaseUI
{
    public int PianoCost;
    public int DrumCost;
    public int StringCost;

    enum EButtons
    {
        NormalTower,
        ExitButton
    }

    enum EImages
    {
        BuildingsPanel
    }

    private Button _normalTower;
    private Button _exitButton;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));
        Bind<Image>(typeof(EImages));

        _normalTower = Get<Button>((int)EButtons.NormalTower);
        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _normalTower.onClick.AddListener(HandleNormalTower);
        _exitButton.onClick.AddListener(HandleExitButton);

        PianoCost = 100;
        DrumCost = 100;
        StringCost = 100;

        return true;
    }

    private void HandleNormalTower()
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
}