using System.Collections.Generic;
using UnityEngine;

public class TitleRootUI : BaseUI, ISetActiveCanvases
{
    private Dictionary<string, Canvas> CanvasDict = new Dictionary<string, Canvas>();

    public TitleCanvas TitleCanvas { get; private set; }
    public OptionCanvas OptionCanvas { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        TitleCanvas = gameObject.FindChild<TitleCanvas>("TitleCanvas", true);
        OptionCanvas = gameObject.FindChild<OptionCanvas>("OptionCanvas", true);

        CanvasDict.Add(TitleCanvas.name, TitleCanvas.GetComponent<Canvas>());
        CanvasDict.Add(OptionCanvas.name, OptionCanvas.GetComponent<Canvas>());

        SetActiveCanvas("TitleCanvas", true);
        SetActiveCanvas("OptionCanvas", false);
        return true;
    }

    public void SetActiveCanvas(string name, bool active)
    {
        CanvasDict[name].gameObject.SetActive(active);
    }
}
