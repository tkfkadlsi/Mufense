using UnityEngine;

public class GameTimer : BaseInit
{
    public int EnemyHPLevel { get; private set; }
    public int EnemySpawnAmountLevel { get; private set; }


    private readonly float _hpLevelCooltime = 150.0f;
    private readonly float _amountLevelCooltime =  230.0f;


    private float _hpLevelCooldown;
    private float _amountLevelCooldown;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        EnemyHPLevel = 0;
        EnemySpawnAmountLevel = 0;

        return true;
    }

    private void Update()
    {
        _hpLevelCooldown += Time.deltaTime;
        _amountLevelCooldown += Time.deltaTime;

        if(_hpLevelCooldown >= _hpLevelCooltime)
        {
            _hpLevelCooldown -= _hpLevelCooltime;
            EnemyHPLevel++;
        }
        if(_amountLevelCooldown >= _amountLevelCooltime)
        {
            _amountLevelCooldown -= _amountLevelCooltime;
            EnemySpawnAmountLevel++;
        }
    }
}
