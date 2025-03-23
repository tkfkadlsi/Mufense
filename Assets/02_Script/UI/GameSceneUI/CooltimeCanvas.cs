using TMPro;
using UnityEngine;

public class CooltimeCanvas : BaseUI, IMusicPlayHandle
{
    private float _enemySpawnLevel;
    private float _enemySpawnAmount;
    private float _enemyHPLevel;
    private float _spawnTreasure;

    private GameTimer _gameTimer;

    enum ETexts
    {
        NearOneEventText,
        NearTwoEventText,
    }

    private TextMeshProUGUI _nearOneEventText;
    private TextMeshProUGUI _nearTwoEventText;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<TextMeshProUGUI>(typeof(ETexts));

        _nearOneEventText = Get<TextMeshProUGUI>((int)ETexts.NearOneEventText);
        _nearTwoEventText = Get<TextMeshProUGUI>((int)ETexts.NearTwoEventText);
        _gameTimer = Managers.Instance.Game.FindBaseInitScript<GameTimer>();

        return true;
    }

    public void SettingColor(Music music)
    {
        Color color = music.TextColor;

        _nearOneEventText.color = color;

        color.a = 0.5f;

        _nearTwoEventText.color = color;
    }

    private void Update()
    {
        (string, float) enemySpawnLevel = ("EnemySpawnLevel", _gameTimer.EnemySpawnLevel);
    }
}
