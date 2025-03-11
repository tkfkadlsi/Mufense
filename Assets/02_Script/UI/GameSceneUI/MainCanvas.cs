using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : BaseUI, IMusicPlayHandle
{
    private enum EButtons
    {
        TowerBuildButton,
        BGMChangeButton
    }

    private Button _towerBuildButton;
    private Button _bgmChangeButton;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));

        _towerBuildButton = Get<Button>((int)EButtons.TowerBuildButton);
        _bgmChangeButton = Get<Button>((int)EButtons.BGMChangeButton);

        _towerBuildButton.onClick.AddListener(HandleTowerBuildButton);
        _bgmChangeButton.onClick.AddListener(HandleBGMChangeButton);

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _towerBuildButton.image.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
    }

    private void HandleTowerBuildButton()
    {
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("BuildCanvas", true);
    }

    private void HandleBGMChangeButton()
    {
        MusicPlayer musicPlayer = Managers.Instance.Game.FindBaseInitScript<MusicPlayer>();
        Music randMusic = musicPlayer.PlayableMusicList[Random.Range(0, musicPlayer.PlayableMusicList.Count)];
        musicPlayer.ChangeMusic(randMusic);
    }

    public void SetBuildButtonActive(bool active)
    {
        _towerBuildButton.gameObject.SetActive(active);
    }

    public void SettingColor(Music music)
    {
        _towerBuildButton.image.DOColor(Managers.Instance.Game.PlayingMusic.PlayerColor, 1f);
    }
}
