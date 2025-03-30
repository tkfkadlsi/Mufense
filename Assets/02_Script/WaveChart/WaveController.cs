using UnityEngine;
using System.Collections.Generic;

public class WaveController : BaseInit
{
    private int _waveCount = 0;
    [SerializeField] private List<WaveDataSO> _waveDatas = new List<WaveDataSO>();

    public WaveDataSO CurrentWave { get; private set; }

    public void StartWave(int value)
    {
        _waveCount = value;
        CurrentWave = _waveDatas[_waveCount];
        CurrentWave.StartWave();
    }

    public void NextWave()
    {
        if(CurrentWave != null)
        {
            CurrentWave.StopWave();
        }

        _waveCount++;

        CurrentWave = _waveDatas[_waveCount];
    }

    public void StopWave()
    {
        CurrentWave.StopWave();
    }
}
