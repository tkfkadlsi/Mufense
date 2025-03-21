using UnityEngine;

public class SpecialTreasure : Treasure
{
    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.UI.GameRootUI.RewardCanvas.FinishReward -= _poolable.PushThisObject;
        }

        base.Release();
    }

    protected override void Reward()
    {
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("RewardCanvas", true);
        Managers.Instance.UI.GameRootUI.RewardCanvas.FinishReward += _poolable.PushThisObject;
    }
}
