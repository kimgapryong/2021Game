using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpwaningIcons
{
   
   public IEnumerator StartSpwanIcons(float seconds)
    {
        
        int maxTool = Manager.Game.maxCount;

        while(true)
        {
            int current = Manager.Game.current;

            if (current <= maxTool)
            {
                yield return new WaitForSeconds(0.1f);

                string[] names = Enum.GetNames(typeof(Define.Icons));
                int randValue = UnityEngine.Random.Range(0, names.Length);

                Type[] icons = new Type[] { typeof(PreportiesIcon) };

                Cell cell = FindSetIcons();

                if (cell.Icons == null)
                {
                    Manager.Game.current++;
                    cell.Icons = Manager.Object.SpwanIcon(new Vector3(cell.x, cell.y), icons[randValue], true);
                }
            }
            else
                yield return null;
        }
    }

    private Cell FindSetIcons()
    {
        GridController grid = Manager.Object.Grid;

        int maxX = Manager.Game.xTile;
        int maxY = Manager.Game.yTile;

        int randPosX = Random.Range(1, maxX - 1);
        int randPosY = Random.Range(1, maxY - 1);

        Cell cell = grid.gridDic[new Vector3Int(randPosX, randPosY)];
        if(cell.Type == Define.TileType.Wall)
            return FindSetIcons();
        
        return cell;
    }
}
