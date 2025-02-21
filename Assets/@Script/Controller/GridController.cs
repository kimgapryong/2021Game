using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell
{
    //TODO 플레이어 인지 적 인지 

    public GameObject obj;
    public int x;
    public int y;

    private Define.TileType _tileType = Define.TileType.Normal;
    public Define.TileType Type {get { return _tileType; }
        set
        {
            if(value == Define.TileType.Normal || value == Define.TileType.Wall || value == Define.TileType.P_Tile)
            {
                _tileType = value;
                obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(_tileType.GetTileName());
                obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            }
            else
            {
                Manager.Game.action?.Invoke(_tileType, value, this);
                _tileType = value;
            }
              
        }
    }
}
public class GridController : BaseController 
{
    //각 백터값마다 cell 관리
    public Dictionary<Vector3Int, Cell> gridDic = new Dictionary<Vector3Int, Cell>();

    private Transform _root;
    public Transform Root
    {
        get
        {
            if(_root == null)
            {
                _root = new GameObject("TileTrans").transform;
            }
            return _root;
        }
    }
       
    

    [SerializeField]
    private int xTile;

    [SerializeField]
    private int yTile;

    [SerializeField]
    private int wallCount = 1;

    //벽의 위치 배수
    private int wallPos;

    //벽 배수 증가값
    private int xWall;
    private int yWall;
    //벽 몇개 증가
    private int plusWall = 20;
    // x / wallCount 
    Grid _grid;
    List<Cell> cells;

    public override bool Init()
    {
        base.Init();

        _grid = gameObject.GetOrAddComponent<Grid>();

        return true;
    }
    public void InitGrid()
    {
        xTile = Manager.Game.xTile;
        yTile = Manager.Game.yTile;
        wallCount = Manager.Game.wallCount;

        wallPos = xTile / wallCount;
        xWall = wallPos;
        yWall = wallPos;

        SetFirstTile();
    }
    private void SetFirstTile()
    {
        for(int x = 0; x < xTile; x++)
        {
            for(int y = 0; y < yTile; y++)
            {
                Cell cell = GetCell(new Vector3Int(x, y));
                cell.x = x;
                cell.y = y;
                cell.Type = Define.TileType.Normal;
            } 
        }
      

        for (int x = 0; x < xTile; x++)
        {
            for (int y = 0; y < yTile; y++)
            {
                Cell cell = GetCell(new Vector3Int(x, y));
                if(x == 0 || x == xTile - 1 || y == 0 || y == yTile - 1)
                {
                    cell.Type = Define.TileType.Wall;
                    continue;
                }
                if(x == xWall && y == yWall)
                {
                   

                    int randX = Random.Range(1, xTile - 1);
                    int randY = Random.Range(1, yTile - 1);

                    int[] xDir = new int[3] { 3, 1, 4 }; // 우 좌 에외
                    int[] yDir = new int[3] { 0, 2, 4 }; // 상 하 예외

                    // randX == 1 우 => 4 randY == 1 위 => 1
                    // randX == xTile - 2 왼 => 2 randY == yTile - 2 아래 => 3
                   
                    if (randX == 1 || randY == 1 || randX == xTile - 2 || randY == yTile - 2)
                    {
                        int dirX = (randX == 1) ? 0 : (randX == xTile - 2) ? 1 : 2;
                        int dirY = (randY == 1) ? 0 : (randY == yTile - 2) ? 1: 2;

                        RandomDirWall(xWall, randY, (Define.TileDir)yDir[dirY]);
                        RandomDirWall(randX, yWall, (Define.TileDir)xDir[dirX]);
                       
                    }

                    int randomX = Random.Range(0, 4);
                    int randomY = Random.Range(0, 4);


                    RandomDirWall(xWall, randY, (Define.TileDir)randomX);
                    RandomDirWall(randX, yWall, (Define.TileDir)randomY);

                    xWall += wallPos;
                    yWall += wallPos;
                }
            }
        }

        for (int x = 1; x <= 3; x++)
        {
            for (int y = 1; y <= 3; y++)
            {
                Cell cell = GetCell(new Vector3Int(x, y));
                cell.Type = Define.TileType.P_Tile;
            }
        }
    }

    //벽 상하좌우 랜덤 설치
    private void RandomDirWall(int x, int y, Define.TileDir dir)
    {
        if (dir == Define.TileDir.None)
            return;

        Vector3Int[] direction = new Vector3Int[4] { Vector3Int.up, Vector3Int.left, Vector3Int.down, Vector3Int.right };

        //불럭을 추가할 방향
        Vector3Int direct = direction[(int)dir];

        //현재 나의 위치
        Vector3Int currentVec = new Vector3Int(x, y);
        Debug.Log("내 위치 : " + currentVec);

        Cell currentCell = GetCell(currentVec);
        currentCell.Type = Define.TileType.Wall;

        for(int i = 0; i < plusWall; i++)
        {
            currentVec += direct;
            currentCell = GetCell(currentVec);

            if (currentVec.x <= 0 || currentVec.y <= 0 || currentVec.x >= xTile - 1 || currentVec.y >= yTile)
                break;
            if (currentCell.Type == Define.TileType.Wall)
                continue;

            currentCell.Type = Define.TileType.Wall;
        }
    }
    public Cell GetCell(Vector3Int vec)
    {
      
        Cell cell = null;
        if(gridDic.TryGetValue(vec, out cell) == false)
        {
            cell = new Cell();
            cell.obj = Manager.Resources.Instantiate("SpriteTile",new Vector3(vec.x, vec.y), Root);
           
            gridDic.Add(vec, cell);
        }
        return cell;
    }

    public void FullCell(Cell start)
    {
        if (start.Type == Define.TileType.P_Start)
        {
            Full_Pstart();
            return;
        }
            
        int count = 0;
        Queue<Cell> cellsQueue = new Queue<Cell>();
        cellsQueue.Enqueue(start);

        while (cellsQueue.Count > 0)
        {
            count++;
            if(count >= 10000)
            {
                Debug.Log("초과초과");
                break;
            }
            Cell current = cellsQueue.Dequeue();
            current.Type = Define.TileType.P_Tile;

            GetCells(current, cellsQueue);
        }

        Full_Pstart();
    }

    private void GetCells(Cell cell, Queue<Cell> cellsQueue)
    {
        Vector3Int first = new Vector3Int(cell.x, cell.y);

        int[] dirY = new int[4] { 1, 0, -1, 0 }; // 상, 좌, 하, 우
        int[] dirX = new int[4] { 0, 1, 0, -1 };

        for (int i = 0; i < dirX.Length; i++)
        {
            int x = dirX[i];
            int y = dirY[i];
            Vector3Int next = new Vector3Int(first.x + x, first.y + y); // 인접 셀
            Cell nextCell = null;


            if (!gridDic.TryGetValue(next, out nextCell) || nextCell == null)
                continue;

            // 인접 셀이 벽이거나 이미 P_Tile인 경우 건너뜁니다.
            if (nextCell.Type == Define.TileType.Wall ||
                nextCell.Type == Define.TileType.P_Tile ||
                nextCell.Type == Define.TileType.P_Start ||
                nextCell.x <= 0 || nextCell.y <= 0 ||
                nextCell.x >= xTile - 1 || nextCell.y >= yTile - 1)
                continue;

            // 큐에 유효한 셀을 추가합니다.
            cellsQueue.Enqueue(nextCell);
        }
    }

    private void Full_Pstart()
    {
        // 큐를 사용하여 셀을 처리합니다.
        Queue<Cell> cellsQueue = new Queue<Cell>(Manager.Input.cells);

        while (cellsQueue.Count > 0)
        {
            Cell current = cellsQueue.Dequeue();
            current.Type = Define.TileType.P_Tile;
        }
    }
}
