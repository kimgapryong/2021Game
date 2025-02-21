using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        Manager.Resources.LoadAllAsync<Object>("PreAll", (name, count, result) =>
        {
            Debug.Log($"{name} / {count} // {result}");
            if(count == result)
            {
                StartGame();
            }
                
        });
    }
   
    void StartGame()
    {
        GameObject ggg = new GameObject("dssdf");
        ggg.AddComponent<SpriteRenderer>().sprite  = Manager.Resources.Load<Sprite>("Non.sprite");

        GridController gc = Manager.Object.Spwan<GridController>(Vector3.zero);
        gc.InitGrid();

        Manager.Object.Spwan<PlayerController>(new Vector3(1,1));
        Manager.Object.Spwan<CameraController>(Vector2.zero);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
