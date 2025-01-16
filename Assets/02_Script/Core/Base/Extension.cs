using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Utility.GetOrAddComponent<T>(go);
    }

    public static bool IsValid(this GameObject go)
    {
        return Utility.IsValid(go);
    }

    public static GameObject FindChild(this GameObject go, string name, bool recursive = false)
    {
        return Utility.FindChild(go, name, recursive);
    }

    public static T FindChild<T>(this GameObject go, string name, bool recursive = false) where T : Object
    {
        return Utility.FindChild<T>(go, name, recursive);
    }
}
