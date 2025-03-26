using UnityEngine;

public class SpeedChange : MonoBehaviour
{
    public void Speed1X()
    {
        Managers.Instance.Game.SetTimeScale(1f);
    }

    public void Speed2X()
    {
        Managers.Instance.Game.SetTimeScale(2f);
    }
}
