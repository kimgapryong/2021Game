using Cinemachine;
using System;
using UnityEngine;

public class CameraController : BaseController
{

    public override bool Init()
    {
        base.Init();
        CinemachineVirtualCamera  vCam = GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = Manager.Object.Player.transform;
        
        vCam.m_Lens.OrthographicSize = 11; // ªÁ¿Ã¡Ó

        vCam.AddCinemachineComponent<CinemachineFramingTransposer>();
        
        return true;
    }

  

}
