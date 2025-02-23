using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    protected int bullet { get; set; }
    private bool first = false;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdataGun();
    }

    protected virtual void UpdataGun()
    {

    }
    protected virtual bool Init()
    {
        if(!first)
        {
            first = true;


            return true;
        }
            
          return false;
    }
    protected virtual void OnClick ()
    {

    }
}
