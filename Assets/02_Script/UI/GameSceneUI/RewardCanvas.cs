using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum RewardType
{
    NewSong = 0,
    StunArc = 1,
    MaxMusicPower = 2,
    CoreHealing = 3
}

public class RewardCanvas : BaseUI, IMusicPlayHandle
{
    [SerializedDictionary("Type", "Reward")]
    public SerializedDictionary<RewardType, Reward> _rewardDictionary = new SerializedDictionary<RewardType, Reward>();

    public event Action FinishReward;

    private RewardType _rewardType;
    private Music _rewardMusic;

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

        _exitButton.onClick.AddListener(HandleExitButton);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        if (Managers.Instance.Game.PlayingMusic == null) return;

        _panel.rectTransform.localScale = Vector3.zero;
        _panel.rectTransform.DOScale(Vector3.one, 0.5f);
        _panel.color = Managers.Instance.Game.PlayingMusic.BackGroundColor;
        _iconBackGround.color = Managers.Instance.Game.PlayingMusic.BackGroundColor;
        _icon.color = Managers.Instance.Game.PlayingMusic.TextColor;
        _titleText.color = Managers.Instance.Game.PlayingMusic.TextColor;
        _descriptionText.color = Managers.Instance.Game.PlayingMusic.TextColor;
        _exitButton.gameObject.SetActive(false);
        StartCoroutine(OpenPanel());
    }

    public void SettingColor(Music music)
    {
        _panel.DOColor(music.BackGroundColor, 1f);
        _iconBackGround.DOColor(music.BackGroundColor, 1f);
        _icon.DOColor(music.TextColor, 1f);
        _titleText.DOColor(music.TextColor, 1f);
        _descriptionText.DOColor(music. TextColor, 1f);
    }

    private IEnumerator OpenPanel()
    {

        for(int i = 0; i < 10; i++)
        {
            _iconBackGround.rectTransform.localScale = Vector3.one * 1.05f;
            _iconBackGround.rectTransform.DOScale(Vector3.one, 0.05f);

            _rewardType = GetRandomReward();
            SettingReward(_rewardType);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        for (int i = 1; i <= 10; i++)
        {
            _iconBackGround.rectTransform.localScale = Vector3.one * 1.05f;
            _iconBackGround.rectTransform.DOScale(Vector3.one, i * 0.05f);

            _rewardType = GetRandomReward();
            SettingReward(_rewardType);
            yield return new WaitForSecondsRealtime(i * 0.1f);
        }
        _rewardType = GetRandomReward();
        SettingReward(_rewardType);
        _exitButton.gameObject.SetActive(true);
    }

    private RewardType GetRandomReward()
    {
        if (Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().MusicList.Count == 0)
        {
            return (RewardType)Random.Range(1, 4);
        }
        else
        {
            return (RewardType)Random.Range(0, 4);
        }
    }

    private void SettingReward(RewardType rewardType)
    {

        switch (rewardType)
        {
            case RewardType.NewSong:
                _titleText.text = _rewardDictionary[RewardType.NewSong].GetName();

                _rewardMusic = Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().MusicList[
                    Random.Range(0, Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().MusicList.Count)];

                if (Managers.Instance.Game.Language == Language.Korean)
                {
                    _descriptionText.text = $"{_rewardMusic.SongName} È¹µæ";
                }
                if(Managers.Instance.Game.Language == Language.English)
                {
                    _descriptionText.text = $"Get {_rewardMusic.SongName}";
                }

                _icon.sprite = _rewardDictionary[RewardType.NewSong].Icon;
                break;
            case RewardType.StunArc:
                _titleText.text = _rewardDictionary[RewardType.StunArc].GetName();
                _descriptionText.text = _rewardDictionary[RewardType.StunArc].GetDescription();
                _icon.sprite = _rewardDictionary[RewardType.StunArc].Icon;
                break;
            case RewardType.MaxMusicPower:
                _titleText.text = _rewardDictionary[RewardType.MaxMusicPower].GetName();
                _descriptionText.text = _rewardDictionary[RewardType.MaxMusicPower].GetDescription();
                _icon.sprite = _rewardDictionary[RewardType.MaxMusicPower].Icon;
                break;
            case RewardType.CoreHealing:
                _titleText.text = _rewardDictionary[RewardType.CoreHealing].GetName();
                _descriptionText.text = _rewardDictionary[RewardType.CoreHealing].GetDescription();
                _icon.sprite = _rewardDictionary[RewardType.CoreHealing].Icon;
                break;
        }
    }

    private void HandleExitButton()
    {
        switch(_rewardType)
        {
            case RewardType.NewSong:

                Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().MusicList.Remove(_rewardMusic);
                Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayableMusicList.Add(_rewardMusic);

                break;

            case RewardType.StunArc:

                Managers.Instance.Game.FindBaseInitScript<Core>().StunArcAttack();

                break;

            case RewardType.MaxMusicPower:

                Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().SetMaxMusicPower(
                    Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().MaxMusicPower + 1000);

                break;

            case RewardType.CoreHealing:

                Managers.Instance.Game.FindBaseInitScript<Core>().Heal(25f);

                break;
        }

        FinishReward?.Invoke();
        
        _panel.rectTransform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            Managers.Instance.UI.GameRootUI.SetActiveCanvas("RewardCanvas", false);
        });

    }
}
