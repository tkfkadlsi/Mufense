using UnityEngine;

public static class Utility
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = null;
        component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static bool IsValid(GameObject go)
    {
        return go != null && go.activeInHierarchy;
    }

    public static GameObject FindChild(GameObject go, string name, bool recursive = false)
    {
        Transform transform = null;
        transform = FindChild<Transform>(go, name, recursive);
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name, bool recursive = false) where T : Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    T component = child.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
}
