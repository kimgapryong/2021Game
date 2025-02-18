using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell
{
    //TODO �÷��̾� ���� �� ���� 

    public GameObject obj;

    private Define.TileType _tileType = Define.TileType.Normal;
    public Define.TileType Type {get { return _tileType; }
        set
        {
            if(value == Define.TileType.Normal || value == Define.TileType.Wall)
            {
                _tileType = value;
                obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(_tileType.GetTileName());
            }
            else
            {
                Manager.Game.action?.Invoke(_tileType, value, this);
            }
              
        }
    }
}
public class GridController : BaseController 
{
    //�� ���Ͱ����� cell ����
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

    //���� ��ġ ���
    private int wallPos;

    //�� ��� ������
    private int xWall;
    private int yWall;
    //�� � ����
    private int plusWall = 20;
    // x / wallCount 
    Grid _grid;

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

                    int[] xDir = new int[3] { 3, 1, 4 }; // �� �� ����
                    int[] yDir = new int[3] { 0, 2, 4 }; // �� �� ����

                    // randX == 1 �� => 4 randY == 1 �� => 1
                    // randX == xTile - 2 �� => 2 randY == yTile - 2 �Ʒ� => 3
                   
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
    }

    //�� �����¿� ���� ��ġ
    private void RandomDirWall(int x, int y, Define.TileDir dir)
    {
        if (dir == Define.TileDir.None)
            return;

        Vector3Int[] direction = new Vector3Int[4] { Vector3Int.up, Vector3Int.left, Vector3Int.down, Vector3Int.right };

        //�ҷ��� �߰��� ����
        Vector3Int direct = direction[(int)dir];

        //���� ���� ��ġ
        Vector3Int currentVec = new Vector3Int(x, y);
        Debug.Log("�� ��ġ : " + currentVec);

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
}
