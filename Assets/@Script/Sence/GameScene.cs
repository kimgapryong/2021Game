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
        GridController gc = Manager.Object.Spwan<GridController>(Vector3.zero);
        gc.InitGrid();

        Manager.Object.Spwan<PlayerController>(new Vector3(1,1));

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
