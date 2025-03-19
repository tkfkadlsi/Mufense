using System;
using UnityEngine;

[Serializable]
public class Reward
{
    [SerializeField] private string KRName;
    [SerializeField] private string KRDescription;

    [SerializeField] private string ENGName;
    [SerializeField] private string ENGDescription;

    public Sprite Icon;
    public string GetName()
    {
        if(Managers.Instance.Game.Language == Language.Korean)
        {
            return KRName;
        }
        if(Managers.Instance.Game.Language == Language.English)
        {
            return ENGName;
        }

        return "";
    }

    public string GetDescription()
    {
        if(Managers.Instance.Game.Language == Language.Korean)
        {
            return KRDescription;
        }
        if(Managers.Instance.Game.Language == Language.English)
        {
            return ENGDescription;
        }

        return "";
    }
}
