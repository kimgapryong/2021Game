using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum TileType
    {
        None,
        Normal,
        P_Tile,
        E_Tile,
        Wall,
    }

    public enum TileDir
    {
        Up = 0,
        Left = 1,
        Down = 2,
        Right = 3,
        None = 4,
    }

    private static Dictionary<TileType, string> tileNameMapping = new Dictionary<TileType, string>
    {
        { TileType.None, "" },
        { TileType.Normal, "Blue_TileSet_Full.sprite" },
        { TileType.P_Tile, "Green_TileSet_Full.sprite" },
        { TileType.E_Tile, "Red_TileSet_Full.sprite" },
        { TileType.Wall, "Purple_TileSet_Full.sprite" }
    };

    public static string GetTileName(TileType type)
    {
        if (tileNameMapping.TryGetValue(type, out string tileName))
        {
            return tileName;
        }
        return null;  // 해당 타입에 맞는 이름이 없다면 null 반환
    }
}
