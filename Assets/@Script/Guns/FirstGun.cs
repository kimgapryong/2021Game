using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGun : BaseGun
{
    GunData gunData;
    protected override bool Init()
    {
        base.Init();
        gunData = Manager.Resources.Load<GunData>("Gun1.data");
        bullet = gunData.bulletCount;

        return true;
    }
}
