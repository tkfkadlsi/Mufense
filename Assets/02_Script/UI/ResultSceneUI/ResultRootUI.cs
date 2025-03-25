using System.Collections.Generic;
using UnityEngine;

public class ResultRootUI : BaseUI, ISetActiveCanvases
{
    private Dictionary<string, Canvas> CanvasDict = new Dictionary<string, Canvas>();

    public ResultCanvas ResultCanvas { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ResultCanvas = gameObject.FindChild<ResultCanvas>("ResultCanvas", true);

        CanvasDict.Add(ResultCanvas.name, ResultCanvas.GetComponent<Canvas>());

        SetActiveCanvas("ResultCanvas", true);

        return true;
    }

    protected override void ActiveOn()
    {
        base.ActiveOn();

        Managers.Instance.Pool.ClearAllPool();
        Managers.Instance.Game.ClearDictionary();
    }

    public void SetActiveCanvas(string name, bool active)
    {
        CanvasDict[name].gameObject.SetActive(active);
    }
}
