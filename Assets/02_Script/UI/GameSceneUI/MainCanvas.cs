using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : BaseUI
{
    private enum EButtons
    {
        TowerBuildButton
    }

    private Button _towerBuildButton;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));

        _towerBuildButton = Get<Button>((int)EButtons.TowerBuildButton);
        _towerBuildButton.onClick.AddListener(HandleTowerBuildButton);

        return true;
    }

    private void HandleTowerBuildButton()
    {
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("BuildCanvas", true);
    }

    public void SetBuildButtonActive(bool active)
    {
        _towerBuildButton.gameObject.SetActive(active);
    }
}
