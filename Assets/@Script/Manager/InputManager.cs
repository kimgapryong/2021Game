using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<Vector3Int, Transform> moveAction;

    private bool str = false;

    public Vector3Int strVec = Vector3Int.zero;

    public List<Cell> cells = new List<Cell>();

    private Cell first;
    public void Init()
    {
        moveAction += PlayerMove;
    }
    public void PlayerMove(Vector3Int vec, Transform trans)
    {
     
      
        Vector3Int vecInt = Vector3Int.FloorToInt(trans.position) + vec;
        Cell cell = Manager.Object.Grid.gridDic[vecInt]; //�÷��̾� ���� ��

        if (vecInt.x <= 0 || vecInt.y <= 0  || vecInt.x >= Manager.Instance.xTile - 1 || vecInt.y >= Manager.Instance.yTile - 1)
            return;
        if(cell.Type == Define.TileType.Wall)
            return;
        if (cell.Type == Define.TileType.P_Start)
            return;
        if(cell.Type == Define.TileType.Normal && first == null)
            first = Manager.Object.Grid.gridDic[Vector3Int.FloorToInt(trans.position)];

        if (!str && first != null)
        {
            strVec = vec;
            str = true;
        }

        trans.position = vecInt;
        if (first == null)
            return;


        //�÷��̾� Ÿ�Ϸ� ����
        if (cell == null)
            return;

        if(cell.Type == Define.TileType.P_Tile) // 1 -> �ּ� �̵��Ͽ� �ҷ��� �̵��� �� �ִ� ��
        {
            Vector3Int nexVec;
            if(strVec.y > 0)
            {
                int result = cell.x - first.x;
                if(result >= 0)
                    nexVec = Vector3Int.right;
                else
                    nexVec = Vector3Int.left;
            }
            else
            {
                int result = cell.y - first.y;
                if(result >= 0)
                    nexVec = Vector3Int.up;
                else
                    nexVec = Vector3Int.down;
            }
            Cell startCell = Manager.Object.Grid.gridDic[new Vector3Int(first.x, first.y) + (strVec + nexVec)];
            Debug.Log(startCell.x + "/" + startCell.y);
            Debug.Log(strVec);
            Debug.Log(nexVec);
            Debug.Log(strVec + nexVec);
            Debug.Log(first.x + "/" + first.y);
            if (startCell == null)
                Debug.LogError("�� ����");

            Manager.Object.Grid.FullCell(startCell);

            str = false;
            first = null;
            
            return;
        }

        cell.Type = Define.TileType.P_Start;
        cells.Add(cell);
    }
}
