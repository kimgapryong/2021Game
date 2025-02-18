using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController
{
    [SerializeField]
    private float moveSpeed = 20f; //플레이어한테 멀어지는 속도

    [SerializeField]
    private float rebackSpeed = 10f; //플레이어한테 돌아오는 속도

    [SerializeField]
    private float backCam = 2f; //플레이어한테 카메라 거리 두는 정도
    public override bool Init()
    {
        base.Init();

        transform.position = Manager.Object.Player.transform.position;

        return true;
    }

    
}
