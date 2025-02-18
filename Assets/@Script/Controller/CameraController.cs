using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController
{
    [SerializeField]
    private float moveSpeed = 20f; //�÷��̾����� �־����� �ӵ�

    [SerializeField]
    private float rebackSpeed = 10f; //�÷��̾����� ���ƿ��� �ӵ�

    [SerializeField]
    private float backCam = 2f; //�÷��̾����� ī�޶� �Ÿ� �δ� ����
    public override bool Init()
    {
        base.Init();

        transform.position = Manager.Object.Player.transform.position;

        return true;
    }

    
}
