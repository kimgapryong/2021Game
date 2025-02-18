using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
   public GridController Grid { get; private set; } 
   public PlayerController Player { get; private set; }

   public T Spwan<T>(Vector3 postion, Transform trans = null) where T : BaseController
    {
        System.Type type = typeof(T);

        if(type == typeof(GridController))
        {
            if(Grid != null)
                return Grid as T;

            GameObject go = new GameObject("@Grid");
            go.transform.position = postion;
            go.AddComponent<Grid>();

            GridController gc = go.AddComponent<GridController>();
            Grid = gc;

            return gc as T;
        }
        else if(type == typeof(PlayerController))
        {
            GameObject go = Manager.Resources.Instantiate("Player");
            go.transform.position = postion;
            go.name.Replace("(Clone)", "");

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;

            return pc as T;
        }


        return null;
    }
}
