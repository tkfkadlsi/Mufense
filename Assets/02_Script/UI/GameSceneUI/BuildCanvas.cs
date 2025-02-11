using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildCanvas : BaseUI
{
    [SerializeField] private Sprite _normalTowerIcon;
    [SerializeField] private Sprite _lineTowerIcon;
    [SerializeField] private Sprite _starTowerIcon;
    [SerializeField] private Sprite _bombTowerIcon;

    enum EButtons
    {
        NormalTower,
        LineTower,
        StarTower,
        BombTower,
        ExitButton
    }

    enum EImages
    {
        BuildingsPanel
    }

    private Button _normalTower;
    private Button _lineTower;
    private Button _starTower;
    private Button _bombTower;
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
        _lineTower = Get<Button>((int)EButtons.LineTower);
        _starTower = Get<Button>((int)EButtons.StarTower);
        _bombTower = Get<Button>((int)EButtons.BombTower);
        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _buildingsPanel = Get<Image>((int)EImages.BuildingsPanel);

        _normalTower.onClick.AddListener(HandleNormalTower);
        _lineTower.onClick.AddListener(HandleLineTower);
        _starTower.onClick.AddListener(HandleStarTower);
        _bombTower.onClick.AddListener(HandleBombTower);
        _exitButton.onClick.AddListener(HandleExitButton);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        StartCoroutine(EnterCoroutine());
    }

    private IEnumerator EnterCoroutine()
    {
        yield return null;
        if(gameObject.activeSelf)
        {
            float endPos = 150f; 
            float lerpTime = 0.5f;
            _normalTower.image.rectTransform.DOMoveY(endPos, lerpTime);
            _lineTower.image.rectTransform.DOMoveY(endPos, lerpTime);
            _starTower.image.rectTransform.DOMoveY(endPos, lerpTime);
            _bombTower.image.rectTransform.DOMoveY(endPos, lerpTime);
            _exitButton.image.rectTransform.DOMoveY(endPos, lerpTime);
        }
    }

    private void HandleNormalTower()
    {
        if (Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().RemoveMusicPower(100) == false) return;
        DisableBuildingButton();
        Managers.Instance.Game.FindBaseInitScript<TowerSpawner>().SetSpawnState(TowerSpawnState.Create, TowerType.Normal);
    }

    private void HandleLineTower()
    {
        DisableBuildingButton();
    }

    private void HandleStarTower()
    {
        DisableBuildingButton();
    }

    private void HandleBombTower()
    {
        DisableBuildingButton();
    }

    private void HandleExitButton()
    {
        StartCoroutine(ExitCoroutine());
    }

    private IEnumerator ExitCoroutine()
    {
        float endPos = -150;
        float lerpTime = 0.5f;

        _normalTower.image.rectTransform.DOMoveY(endPos, lerpTime);
        _lineTower.image.rectTransform.DOMoveY(endPos, lerpTime);
        _starTower.image.rectTransform.DOMoveY(endPos, lerpTime);
        _bombTower.image.rectTransform.DOMoveY(endPos, lerpTime);
        _exitButton.image.rectTransform.DOMoveY(endPos, lerpTime);

        yield return new WaitForSeconds(lerpTime);

        Managers.Instance.UI.GameRootUI.SetActiveCanvas("BuildCanvas", false);
    }

    private void DisableBuildingButton()
    {
        Managers.Instance.UI.GameRootUI.MainCanvas.SetBuildButtonActive(false);
    }
}
