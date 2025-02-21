using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private int playerSuccsec;
    private int playerScore;
    private int enemyScore;

    public int xTile;
    public int yTile;
    public int wallCount = 1;
   
    public Action<Define.TileType, Define.TileType, Cell> action;

    public void Init(int xTile, int yTile, int wallCount )
    {
        this.xTile = xTile;
        this.yTile = yTile; 
        this.wallCount = wallCount;

        //플레이어가 통과해야하는 불럭의 수w
        playerScore = ((xTile * yTile) - (wallCount * 20)) / 100 * 80;

        action += AddTileScroe;
    }

    void AddTileScroe(Define.TileType beforeType, Define.TileType currentType, Cell cell)
    {
        switch (beforeType)
        {
            case Define.TileType.Normal:
                switch (currentType)
                {
                    case Define.TileType.P_Start:
                        playerScore++;
                        cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                        cell.obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                        break;

                    case Define.TileType.E_Tile:
                        enemyScore++;
                        cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                        break;
                }
                break;
            case Define.TileType.P_Start:
                if(currentType == Define.TileType.E_Tile)
                {
                    playerScore--;
                    enemyScore++;
                    cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                    cell.obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                }
                break;
            case Define.TileType.E_Tile:
                if(currentType == Define.TileType.P_Start)
                {
                    playerScore++;
                    enemyScore--;
                    cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                }
                break;
        }
    }
}
