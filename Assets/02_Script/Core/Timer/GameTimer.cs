using UnityEngine;

public class GameTimer : BaseInit
{
    public int EnemyHPLevel { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        EnemyHPLevel = 0;

        return true;
    }

    public void UpgradeEnemyHPLevel()
    {
        EnemyHPLevel++;
    }
}
