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
        GridController gc = Manager.Object.SpwanController<GridController>(Vector3.zero);
        gc.InitGrid();

        Manager.Object.SpwanController<PlayerController>(new Vector3(1,1));
        Manager.Object.SpwanController<CameraController>(Vector2.zero);

        SpwaningIcons spwan = new SpwaningIcons();
        StartCoroutine(spwan.StartSpwanIcons(Manager.Game.spwanTime));

    }
    
}
