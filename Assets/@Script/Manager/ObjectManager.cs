using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ObjectManager
{
   public GridController Grid { get; private set; } 
   public PlayerController Player { get; private set; }
    public HashSet<IconsController> Icons { get; private set; } = new HashSet<IconsController>();   

   public T SpwanController<T>(Vector3 postion, Transform trans = null, bool pool = false) where T : BaseController
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
            GameObject go = Manager.Resources.Instantiate("Player", pooling: pool);
            go.transform.position = postion;
            go.name.Replace("(Clone)", "");

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;

            return pc as T;
        }else if(type == typeof(CameraController))
        {
            GameObject cam = new GameObject("VirtualCam");

            cam.GetOrAddComponent<CinemachineVirtualCamera>();

            cam.GetOrAddComponent<CameraController>();
            
            return cam as T;
        }
        return null;
    }

    public GameObject SpwanIcon(Vector3 pos, Type type, bool pool)
    {
        if (!type.IsSubclassOf(typeof(IconsController)))
            return null;

        GameObject go = Manager.Resources.Instantiate("SpriteTile", pooling: pool);
        go.transform.position = pos;
        go.GetComponent<SpriteRenderer>().sortingOrder = 201;
        if (type == typeof(PreportiesIcon))
        {
            PreportiesIcon icon = go.GetOrAddComponent<PreportiesIcon>();
            Icons.Add(icon);

            return go;
        }

        return null;
    }

    public void Despwan<T>(GameObject obj) 
    {
        System.Type type = typeof(T);
        if(type == typeof(PlayerController))
        {
            Player = null;
            Manager.Resources.Destroy(obj);
        }
        else if(type == typeof(GridController))
        {
            Grid = null;
            Manager.Resources.Destroy(obj);
        }
        else if (type.IsSubclassOf(typeof(IconsController)))
        {
            Icons.Remove(obj.GetComponent<IconsController>());
            Manager.Resources.Destroy(obj);
        }
    }
}
