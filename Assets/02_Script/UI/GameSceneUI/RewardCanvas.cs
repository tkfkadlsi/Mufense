using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType
{
    NewSong = 0,
    SlowWave = 1,

}

public class RewardCanvas : BaseUI, IMusicPlayHandle
{
    [SerializedDictionary("Type", "Reward")]
    public SerializedDictionary<RewardType, Reward> _rewardDictionary = new SerializedDictionary<RewardType, Reward>();

    enum EImages
    {
        Panel,
        IconBackGround,
        Icon
    }

    enum ETexts
    {
        TitleText,
        DescriptionText,
    }

    enum EButtons
    {
        ExitButton
    }

    private Image _panel;
    private Image _iconBackGround;
    private Image _icon;

    private TextMeshProUGUI _titleText;
    private TextMeshProUGUI _descriptionText;

    private Button _exitButton;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Image>(typeof(EImages));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));

        _panel = Get<Image>((int)EImages.Panel);
        _iconBackGround = Get<Image>((int)EImages.IconBackGround);
        _icon = Get<Image>((int)EImages.Icon);

        _titleText = Get<TextMeshProUGUI>((int)ETexts.TitleText);
        _descriptionText = Get<TextMeshProUGUI>((int)ETexts.DescriptionText);

        _exitButton = Get<Button>((int)EButtons.ExitButton);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        _panel.color = Managers.Instance.Game.PlayingMusic.BackGroundColor;
        _iconBackGround.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
        Managers.Instance.Game.SetTimeScale(0);
        StartCoroutine(OpenPanel());
    }

    public void SettingColor(Music music)
    {
        _panel.DOColor(music.BackGroundColor, 1f);
        _iconBackGround.DOColor(music.PlayerColor, 1f);
    }

    private IEnumerator OpenPanel()
    {
        RewardType rewardType = RewardType.NewSong;
        for (int i = 1; i <= 10; i++)
        {
            rewardType = (RewardType)Random.Range(0, 3);
            SettingReward(rewardType);
            yield return new WaitForSecondsRealtime(i * 0.1f);
        }

        switch(rewardType)
        {
            case RewardType.NewSong:
                {

                }
                break;
        }
    }

    private void SettingReward(RewardType rewardType)
    {

        switch (rewardType)
        {
            case RewardType.NewSong:
                _titleText.text = _rewardDictionary[RewardType.NewSong].GetName();
                _descriptionText.text = _rewardDictionary[RewardType.NewSong].GetDescription();
                _icon.sprite = _rewardDictionary[RewardType.NewSong].Icon;
                break;
        }
    }

    private void HandleExitButton()
    {

    }
}
