using TMPro;
using UnityEngine;

public class CooltimeCanvas : BaseUI, IMusicPlayHandle
{
    enum EventType
    {
        EnemySpawnLevel = 0,
        EnemySpawnAmount = 1,
        EnemyHPLevel = 2,
        SpawnTreasure = 3
    }

    private GameTimer _gameTimer;

    enum ETexts
    {
        NearOneEventText,
    }

    private TextMeshProUGUI _nearOneEventText;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<TextMeshProUGUI>(typeof(ETexts));

        _nearOneEventText = Get<TextMeshProUGUI>((int)ETexts.NearOneEventText);
        _gameTimer = Managers.Instance.Game.FindBaseInitScript<GameTimer>();

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void ActiveOff()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.ActiveOff();
    }

    public void SettingColor(Music music)
    {
        _nearOneEventText.color = music.TextColor;
    }

    private void Update()
    {
        (EventType, float) enemySpawnLevel = (EventType.EnemySpawnLevel, _gameTimer.GetSpawnLevelCooldown());
        (EventType, float) enemySpawnAmount = (EventType.EnemySpawnAmount, _gameTimer.GetSpawnAmountCooldown());
        (EventType, float) enemyHPLevel = (EventType.EnemyHPLevel, _gameTimer.GetHPLevelCooldown());
        (EventType, float) spawnTreasure = (EventType.SpawnTreasure, _gameTimer.GetTreasureCooldown());

        (EventType, float) win1;
        (EventType, float) win2;
        (EventType, float) lose1;
        (EventType, float) lose2;

        if (enemySpawnLevel.Item2 > enemySpawnAmount.Item2)
        {
            win1 = enemySpawnAmount;
            lose1 = enemySpawnLevel;
        }
        else
        {
            win1 = enemySpawnLevel;
            lose1 = enemySpawnAmount;
        }

        if (enemyHPLevel.Item2 > spawnTreasure.Item2)
        {
            win2 = spawnTreasure;
            lose2 = enemyHPLevel;
        }
        else
        {
            win2 = enemyHPLevel;
            lose2 = spawnTreasure;
        }

        win1 = win1.Item2 > win2.Item2 ? win2 : win1;
        lose1 = lose1.Item2 > lose2.Item2 ? lose2 : lose1;
        win2 = win2.Item2 > lose1.Item2 ? lose1 : win2;
        win1 = win1.Item2 > win2.Item2 ? win2 : win1;

        if (Managers.Instance.Game.Language == Language.Korean)
        {
            _nearOneEventText.text = $"다음 이벤트 | {GetText(win1.Item1)} - {Mathf.Round(win1.Item2)}초";
        }
        else if (Managers.Instance.Game.Language == Language.English)
        {
            _nearOneEventText.text = $"Next Event | {GetText(win1.Item1)} - {Mathf.Round(win1.Item2)}sec";
        }
    }

    private string GetText(EventType type)
    {
        if (Managers.Instance.Game.Language == Language.Korean)
        {
            switch (type)
            {
                case EventType.EnemySpawnLevel: return "새로운 적 추가";
                case EventType.EnemySpawnAmount: return "적 소환 수 증가";
                case EventType.EnemyHPLevel: return "적 체력 증가";
                case EventType.SpawnTreasure: return "메일 수신";
            }
        }
        else if (Managers.Instance.Game.Language == Language.English)
        {
            switch (type)
            {
                case EventType.EnemySpawnLevel: return "Add New Enemy";
                case EventType.EnemySpawnAmount: return "Increase Enemy Spawn Amount";
                case EventType.EnemyHPLevel: return "Increase Enemy HP";
                case EventType.SpawnTreasure: return "Receive Mail";
            }
        }

        return null;
    }
}
