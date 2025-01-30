using UnityEngine;

public class Core : Unit
{
    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Core;

        return true;
    }

    private void Update()
    {
        transform.Rotate(0, 0, (90f / Managers.Instance.Game.UnitTime) * Time.deltaTime);
    }

    protected override void Setting()
    {
        base.Setting();
    }
}