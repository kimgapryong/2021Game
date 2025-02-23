using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IconsController : BaseController
{
    public CretureController _owner;
    public override bool Init()
    {
        base.Init();

        return true;
    }

    public abstract void OnSetEquipment<T>(CretureController owner);
}
