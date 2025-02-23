using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region ���� ����
    private int playerSuccsec;
    private int playerScore;
    private int enemyScore;
    #endregion

    #region �׸��� 
    public int xTile;
    public int yTile;
    public int wallCount = 1;
    #endregion

    #region ���� ����
    public float spwanTime { get; private set; } = 3.0f;
    public int maxCount { get; private set; } = 10;
    public int current { get; set; } = 0;
    #endregion

    public Action<Define.TileType, Define.TileType, Cell> action;

    public void Init(int xTile, int yTile, int wallCount )
    {
        this.xTile = xTile;
        this.yTile = yTile; 
        this.wallCount = wallCount;

        //�÷��̾ ����ؾ��ϴ� �ҷ��� ��w
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
                    case Define.TileType.P_Tile:
                        playerScore++;
                        cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                        break;

                    case Define.TileType.E_Tile:
                        enemyScore++;
                        cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                        break;
                }
                break;
            case Define.TileType.P_Tile:
                if(currentType == Define.TileType.E_Tile)
                {
                    playerScore--;
                    enemyScore++;
                    cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                }
                break;
            case Define.TileType.E_Tile:
                if(currentType == Define.TileType.P_Tile)
                {
                    playerScore++;
                    enemyScore--;
                    cell.obj.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>(currentType.GetTileName());
                }
                break;
        }
    }
}
