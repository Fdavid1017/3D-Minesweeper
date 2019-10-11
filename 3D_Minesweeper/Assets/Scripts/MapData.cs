using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public int mapX;
    public int mapZ;
    public sbyte[,] numbersMap;
    public bool[,] revealedMap;
    public bool[,] flaggedMap;
    public int playTime;
    public int bombCount;
    public MapGenerations.Difficulity difficulity;
    public int revealedCount = 0;

    public MapData(int mapX, int mapZ, sbyte[,] numbersMap, bool[,] revealedMap, int playTime, int bombCount, MapGenerations.Difficulity difficulity, bool[,] flaggedMap)
    {
        this.mapX = mapX;
        this.mapZ = mapZ;
        this.numbersMap = numbersMap;
        this.revealedMap = revealedMap;
        this.playTime = playTime;
        this.bombCount = bombCount;
        this.difficulity = difficulity;
        this.flaggedMap = flaggedMap;

        for (int x = 0; x < mapX; x++)
        {
            for (int z = 0; z < mapZ; z++)
            {
                if (revealedMap[x, z])
                {
                    revealedCount++;
                }
            }
        }
    }

    public override string ToString()
    {
        return "X-size: " + mapX + ", Z-size: " + mapZ + "\nnumbersMap: " + numbersMap.ToString() + "\nRevealedMap: " + revealedMap.ToString() +
            "\nPlay time: " + playTime + ", Bomb count: " + bombCount + ", Difficulity: " + difficulity;
    }
}
