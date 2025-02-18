using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    
    public int xTile = 50;
    public int yTile = 50;
    public int wall = 3;

    private static Manager _manager;
    public static Manager Instance { get { Init(); return _manager; } }

    private ResourcesManager _resources = new ResourcesManager();
    public static ResourcesManager Resources { get { return Instance?._resources; } }

    private ObjectManager _object = new ObjectManager();
    public static ObjectManager Object { get { return Instance?._object; } }

    private GameManager _game = new GameManager();
    public static GameManager Game { get {  return Instance?._game; } }

    private InputManager _input = new InputManager();
    public static InputManager Input { get {  return Instance?._input; } }
    private void Awake()
    {
        Init();
        ManagerInit();
    }
    static void Init()
    {
        if (_manager == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject("@Manager");
                go.AddComponent<Manager>();
            }
            _manager = go.GetComponent<Manager>();
            DontDestroyOnLoad(go);
        }
    }

    void ManagerInit()
    {
        Debug.Log("메니저 초기화 시작");
        Game.Init(xTile, yTile, wall);
        Input.Init();
    }
}
