using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<Vector3Int, Transform> moveAction;

    private bool str = false;
    private bool non = false;
    public Vector3Int strVec = Vector3Int.zero;
    public Vector3Int nonVec;

    public List<Cell> cells = new List<Cell>();

    private Cell first;
    public void Init()
    {
        moveAction += PlayerMove;
    }
    public void PlayerMove(Vector3Int vec, Transform trans)
    {
        if (strVec != vec && !non && str)
        {
            nonVec = vec;
            non = true;

        }

        if (!str && first != null)
        {
            strVec = vec;
            str = true;
        }
            
      
        
        Vector3Int vecInt = Vector3Int.FloorToInt(trans.position) + vec;
        Cell cell = Manager.Object.Grid.gridDic[vecInt]; //플레이어 다음 셀

        if (vecInt.x <= 0 || vecInt.y <= 0  || vecInt.x >= Manager.Instance.xTile - 1 || vecInt.y >= Manager.Instance.yTile - 1)
            return;
        if(cell.Type == Define.TileType.Wall)
            return;
        if (cell.Type == Define.TileType.P_Start)
            return;
        if(cell.Type == Define.TileType.Normal && first == null)
            first = Manager.Object.Grid.gridDic[Vector3Int.FloorToInt(trans.position)];
      

        trans.position = vecInt;
        if (first == null)
            return;


        //플레이어 타일로 변경
        if (cell == null)
            return;

        if(cell.Type == Define.TileType.P_Tile) // 1 -> 최소 이동하여 불럭을 이동할 수 있는 수
        {

            Cell startCell = Manager.Object.Grid.gridDic[new Vector3Int(first.x, first.y) + (strVec + nonVec)];
            Debug.Log(startCell.x + "/" + startCell.y);
            Debug.Log(strVec);
            Debug.Log(nonVec);
            Debug.Log(strVec + nonVec);
            Debug.Log(first.x + "/" + first.y);
            if (startCell == null)
                Debug.LogError("셀 없음");

            Manager.Object.Grid.FullCell(startCell);

            str = false;
            non = false;
            first = null;
            
            return;
        }

        cell.Type = Define.TileType.P_Start;
        cells.Add(cell);
    }
}
