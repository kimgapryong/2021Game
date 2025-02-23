using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunDatable")]
public class GunData : ScriptableObject
{
    public int bulletCount;
    public int range;
}
