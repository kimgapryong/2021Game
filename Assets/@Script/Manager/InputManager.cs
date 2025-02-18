using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<Vector3Int, Transform> moveAction;
    private bool str = false;
    public Vector3Int strVec;
    public void Init()
    {
        moveAction += PlayerMove;
    }
    public void PlayerMove(Vector3Int vec, Transform trans)
    {
        if(str)
            strVec = vec;

        Vector3Int vecInt = Vector3Int.FloorToInt(trans.position) + vec;
        Cell cell = Manager.Object.Grid.gridDic[vecInt];

        if (vecInt.x <= 0 || vecInt.y <= 0  || vecInt.x >= Manager.Instance.xTile - 1 || vecInt.y >= Manager.Instance.yTile - 1)
            return;
        if(cell.Type == Define.TileType.Wall)
            return;

        trans.position = vecInt;


        //플레이어 타일로 변경
        if (cell == null)
            return;

        if(cell.Type == Define.TileType.P_Tile)
        {

            return;
        }
        cell.Type = Define.TileType.P_Tile;
    }
}
