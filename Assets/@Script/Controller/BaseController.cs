using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    bool reset = false;
    private void Awake()
    {
        Init();
    }
    public virtual bool Init()
    {
        if (!reset)
        {
            reset = true;
            return true;
        }
        return false;
    }
    private void Update()
    {
        UpdateMehod();
    }
    public virtual void UpdateMehod() { }
}
