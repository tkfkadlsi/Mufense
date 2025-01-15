using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RootUI RootUI { get; private set; }

    public void SetRootUI(RootUI rootUI)
    {
        this.RootUI = rootUI;
    }


}
