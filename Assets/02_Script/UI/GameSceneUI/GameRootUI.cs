using System.Collections.Generic;
using UnityEngine;

public class GameRootUI : BaseUI, ISetActiveCanvases
{
    private Dictionary<string, Canvas> CanvasDict = new Dictionary<string, Canvas>();

    public MainCanvas MainCanvas { get; private set; }
    public BuildCanvas BuildingsCanvas { get; private set; }
    public OptionCanvas OptionCanvas { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        MainCanvas = gameObject.FindChild<MainCanvas>("MainCanvas", true);
        BuildingsCanvas = gameObject.FindChild<BuildCanvas>("BuildCanvas", true);
        OptionCanvas = gameObject.FindChild<OptionCanvas>("OptionCanvas", true);

        CanvasDict.Add(MainCanvas.name, MainCanvas.GetComponent<Canvas>());
        CanvasDict.Add(BuildingsCanvas.name, BuildingsCanvas.GetComponent<Canvas>());
        CanvasDict.Add(OptionCanvas.name, OptionCanvas.GetComponent<Canvas>());

        SetActiveCanvas("MainCanvas", true);
        SetActiveCanvas("BuildCanvas", false);
        SetActiveCanvas("OptionCanvas", false);
        return true;
    }

    public void SetActiveCanvas(string name, bool active)
    {
        CanvasDict[name].gameObject.SetActive(active);
    }
}
