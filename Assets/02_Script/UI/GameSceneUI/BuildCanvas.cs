using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildCanvas : BaseUI
{
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

    private Image _buildingsPanel;

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

        _buildingsPanel = Get<Image>((int)EImages.BuildingsPanel);

        _normalTower.onClick.AddListener(HandleNormalTower);
        _exitButton.onClick.AddListener(HandleExitButton);

        return true;
    }

    private void HandleNormalTower()
    {
        DisableBuildingButton();
        HandleExitButton();
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