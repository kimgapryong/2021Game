using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    //이동
    public Action<Vector3Int, Transform> moveAction;

    //클릭
    public Action clickAction;

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

        if(cell.Icons != null)
            

        cell.Type = Define.TileType.P_Tile;
    }
}
