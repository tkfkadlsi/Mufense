using System.Collections.Generic;
using UnityEngine;

public class RootUI : BaseUI
{
    public Dictionary<string, Canvas> Canvases { get; private set; } = new Dictionary<string, Canvas>();

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Canvas[] canvases = GetComponentsInChildren<Canvas>();

        Managers.Instance.Game.SetRootUI(this);

        return true;
    }
}
