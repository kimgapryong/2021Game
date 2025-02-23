using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CretureController
{
    float time = 0.1f;
    Coroutine _cor;
    KeyCode _key;

    
    CameraController cam;
    public override bool Init()
    {
        base.Init();
        cam = Camera.main.GetComponent<CameraController>(); 
        return true;
    }

    private void Update()
    {
        if(_cor == null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _key = KeyCode.W;
                _cor = StartCoroutine(StartEnumerator(time, Vector3Int.up));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _key = KeyCode.S;
                _cor = StartCoroutine(StartEnumerator(time, Vector3Int.down));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _key = KeyCode.D;
                _cor = StartCoroutine(StartEnumerator(time, Vector3Int.right));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _key = KeyCode.A;
                _cor = StartCoroutine(StartEnumerator(time, Vector3Int.left));
            }
        }
        if(Input.GetKeyUp(_key))
        {
            
        }
    }

    IEnumerator StartEnumerator(float tilme, Vector3Int vec)
    {
       yield return new WaitForSeconds(tilme);
       Manager.Input.moveAction?.Invoke(vec, transform);
       _cor = null;
    }
}
