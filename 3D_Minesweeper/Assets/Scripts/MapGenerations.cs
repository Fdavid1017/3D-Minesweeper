using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapGenerations : MonoBehaviour
{
    static public int xSize = 10;
    static public int zSize = 10;
    static public int bombCount = 10;
    static public Difficulity difficulity = Difficulity.Medium;
    static public bool LoadMap = false;
    static public MapData data = null;
    public GameObject winUI;
    public GameObject baseTile;
    public GameObject frameTileInner;
    public GameObject frameTileOuter;
    public GameObject fencePref1;
    public GameObject fencePref2;
    public GameObject player;
    public GameObject water;
    public GameObject ground;

    [HideInInspector]
    public sbyte[,] map;
    [HideInInspector]
    public GameObject[,] tiles;

    [SerializeField]
    GameObject diedUI;

    public enum Difficulity
    {
        Easy, Medium, Hard, Costum
    }

    [SerializeField]
    List<GameObject> smallRocks = new List<GameObject>();

    public static int revealedTilesCount = 0;
    static int tileCount;
    GameObject tilesParent;
    GameObject rocksParent;
    GameObject frameParent;
    GameObject fenceParent;

    // Start is called before the first frame update
    void Start()
    {

        tilesParent = new GameObject();
        tilesParent.name = "Tiles Parent";
        rocksParent = new GameObject();
        rocksParent.name = "Rocks Parent";
        frameParent = new GameObject();
        frameParent.name = "Frame Parent";
        fenceParent = new GameObject();
        fenceParent.name = "Fence Parent";

        revealedTilesCount = 0;
        int smallRocksCount = 0;

        if (LoadMap && data.revealedCount > 0 && data != null)
        {


            difficulity = data.difficulity;
            xSize = data.mapX;
            zSize = data.mapZ;
            bombCount = smallRocksCount = data.bombCount;
            map = data.numbersMap;
            tileCount = xSize * zSize;
            tiles = new GameObject[xSize, zSize];
            revealedTilesCount = data.revealedCount;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < zSize; j++)
                {
                    SpawnTile(i, j, data.revealedMap[i, j], data.flaggedMap[i, j]);
                }
            }
        }
        else
        {
            switch (difficulity)
            {
                case Difficulity.Easy:
                    xSize = zSize = 8;
                    bombCount = 10;
                    smallRocksCount = 10;
                    break;
                case Difficulity.Medium:
                    xSize = zSize = 16;
                    bombCount = 40;
                    smallRocksCount = 40;
                    break;
                case Difficulity.Hard:
                    xSize = 16;
                    zSize = 30;
                    bombCount = 99;
                    smallRocksCount = 99;
                    break;
                case Difficulity.Costum:
                    break;
                default:
                    break;
            }
            map = new sbyte[xSize, zSize];
            tiles = new GameObject[xSize, zSize];
            tileCount = xSize * zSize;
            revealedTilesCount = 0;

            // GenerateBombs();
            // GenerateNumbers();
            SpawnMap();
        }

        SpawnSmallRocks(smallRocksCount);
        GenerateFrame();
        GenerateFences();

        Vector3 center = new Vector3(Mathf.RoundToInt(xSize / 2), 0.5f, Mathf.RoundToInt(zSize / 2));
        player.transform.position = center;

        tiles[Mathf.RoundToInt(center.x), Mathf.RoundToInt(center.z)].gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_9DB77AC7", .5f);

        center.y = -0.25f;
        water.transform.position = center;
        center.y = -5f;
        ground.transform.position = center;
    }

    void SpawnMap()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                SpawnTile(i, j, false, false);
            }
        }
    }

    public void GenerateBombs(int currentX, int currentZ)
    {
        for (int i = 0; i < bombCount; i++)
        {
            sbyte x;
            sbyte z;
            bool gen = true;
            do
            {
                x = (sbyte)Mathf.RoundToInt(UnityEngine.Random.Range(0, xSize));
                z = (sbyte)Mathf.RoundToInt(UnityEngine.Random.Range(0, zSize));
                bool nearby = false;

                for (int l = currentX - 1; l <= currentX + 1 && !nearby; l++)
                {
                    for (int j = currentZ - 1; j <= currentZ + 1 && !nearby; j++)
                    {
                        if (l >= 0 && l < xSize && j >= 0 && j < zSize && (l != x || j != z))
                        {
                            if (x == l || z == j)
                            {
                                nearby = true;
                            }
                        }
                    }
                }

                if (!nearby)
                {
                    if (map[x, z] != -1)
                    {
                        map[x, z] = -1;
                        gen = false;
                    }
                    else
                    {
                        gen = true;
                    }
                }
                else
                {
                    gen = true;
                }
            } while (gen);
        }
        GenerateNumbers();
    }

    void GenerateNumbers()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                if (map[x, z] != -1)
                {
                    map[x, z] = CheckSurroundingBombCount(x, z);
                }
                tiles[x, z].GetComponent<Tile>().NearbyCount = map[x, z];
            }
        }
    }

    void SpawnTile(int x, int z, bool revealed, bool flagged)
    {
        tiles[x, z] = Instantiate(baseTile);
        tiles[x, z].transform.position = new Vector3(x, 0, z);
        tiles[x, z].transform.parent = tilesParent.transform;
        tiles[x, z].name = tiles[x, z].transform.position.ToString();
        tiles[x, z].GetComponent<Tile>().NearbyCount = map[x, z];
        tiles[x, z].GetComponent<Tile>().diedScreenUi = diedUI;
        tiles[x, z].GetComponent<Tile>().Revealed = revealed;
        if (flagged)
        {
            tiles[x, z].GetComponent<Tile>().isFlagged = flagged;
            tiles[x, z].GetComponent<Tile>().FlagModell(true);
        }
    }

    sbyte CheckSurroundingBombCount(int cordX, int cordY)
    {
        /*
            x-1,z-1  x,z-1   x+1,z-1
            x-1,z    x,z     x+1,z
            x-1,z+1  x,z+1   x+1,z+1
        */
        sbyte count = 0;

        for (int i = cordX - 1; i <= cordX + 1; i++)
        {
            for (int j = cordY - 1; j <= cordY + 1; j++)
            {
                if (i >= 0 && i < xSize && j >= 0 && j < zSize && (i != cordX || j != cordY) && map[i, j] == -1)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void RevealNearbyTiles(int cordX, int cordY)
    {
        for (int i = cordX - 1; i <= cordX + 1; i++)
        {
            for (int j = cordY - 1; j <= cordY + 1; j++)
            {
                if (i >= 0 && i < xSize && j >= 0 && j < zSize && (i != cordX || j != cordY) && !tiles[i, j].GetComponent<Tile>().Revealed &&
                    !tiles[i, j].GetComponent<Tile>().isFlagged)
                {
                    tiles[i, j].GetComponent<Tile>().Revealed = true;

                    IncreaseRevelaedTileCount(1);

                    if (tiles[i, j].GetComponent<Tile>().NearbyCount == 0)
                    {
                        RevealNearbyTiles(i, j);
                    }
                }
            }
        }
    }

    void DebugLogMap()
    {
        string outS = "\n";
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                outS += map[x, z].ToString() + "\t";
            }
            outS += "\n";
        }
        Debug.Log(outS);
    }

    public void IncreaseRevelaedTileCount(int increaseBy)
    {
        revealedTilesCount += increaseBy;
        if (revealedTilesCount == (tileCount - bombCount))
        {
            winUI.SetActive(true);
            GameUIHelper.StopTime = true;
        }
    }

    public void SpawnSmallRocks(int rockCount)
    {
        for (int i = 0; i < rockCount; i++)
        {
            int rockNumber = UnityEngine.Random.Range(0, smallRocks.Count);
            Vector3 position = new Vector3(UnityEngine.Random.RandomRange(0f, (float)xSize), 0.49f, UnityEngine.Random.RandomRange(0f, (float)zSize));
            float size1 = UnityEngine.Random.RandomRange(0.25f, 0.4f);
            Vector3 size = new Vector3(size1, size1, size1);
            GameObject rock = Instantiate(smallRocks[rockNumber], rocksParent.transform);
            rock.transform.localScale = size;
            rock.transform.position = position;
            rock.name = rock.transform.position.ToString();
        }
    }

    void GenerateFrame()
    {
        //bottom
        for (int i = -1; i < xSize + 1; i++)
        {
            GameObject cube = Instantiate(frameTileInner, frameParent.transform);
            cube.transform.position = new Vector3(i, 0, -1);
            cube.name = cube.transform.position.ToString();
            cube = Instantiate(frameTileOuter, frameParent.transform);
            cube.transform.position = new Vector3(i, 0, -2);
            cube.name = cube.transform.position.ToString();
        }

        //left
        for (int i = -1; i < zSize + 1; i++)
        {
            GameObject cube = Instantiate(frameTileInner, frameParent.transform);
            cube.transform.position = new Vector3(-1, 0, i);
            cube.name = cube.transform.position.ToString();
            cube = Instantiate(frameTileOuter, frameParent.transform);
            cube.transform.position = new Vector3(-2, 0, i);
            cube.name = cube.transform.position.ToString();
        }

        //top
        for (int i = -1; i < xSize + 1; i++)
        {
            GameObject cube = Instantiate(frameTileInner, frameParent.transform);
            cube.transform.position = new Vector3(i, 0, zSize);
            cube.name = cube.transform.position.ToString();
            cube = Instantiate(frameTileOuter, frameParent.transform);
            cube.transform.position = new Vector3(i, 0, zSize + 1);
            cube.name = cube.transform.position.ToString();
        }

        //right
        for (int i = -1; i < zSize + 1; i++)
        {
            GameObject cube = Instantiate(frameTileInner, frameParent.transform);
            cube.transform.position = new Vector3(xSize, 0, i);
            cube.name = cube.transform.position.ToString();
            cube = Instantiate(frameTileOuter, frameParent.transform);
            cube.transform.position = new Vector3(xSize + 1, 0, i);
            cube.name = cube.transform.position.ToString();
        }

        //edge bottom left
        GameObject cube2 = Instantiate(frameTileOuter, frameParent.transform);
        cube2.transform.position = new Vector3(-2, 0, -2);
        cube2.name = cube2.transform.position.ToString();

        //edge bottom right
        cube2 = Instantiate(frameTileOuter, frameParent.transform);
        cube2.transform.position = new Vector3(xSize + 1, 0, -2);
        cube2.name = cube2.transform.position.ToString();

        //edge top left
        cube2 = Instantiate(frameTileOuter, frameParent.transform);
        cube2.transform.position = new Vector3(-2, 0, zSize + 1);
        cube2.name = cube2.transform.position.ToString();

        //edge top right
        cube2 = Instantiate(frameTileOuter, frameParent.transform);
        cube2.transform.position = new Vector3(xSize + 1, 0, zSize + 1);
        cube2.name = cube2.transform.position.ToString();
    }

    void GenerateFences()
    {
        //bottom
        for (int i = 0; i < xSize; i++)
        {
            GameObject fence = Instantiate(UnityEngine.Random.Range(0, 2) == 0 ? fencePref1 : fencePref2, fenceParent.transform);
            fence.transform.position = new Vector3(i, 1, -1);
            fence.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
            fence.transform.Rotate(0f, -90f, 0f, 0f);
        }

        //left
        for (int i = 0; i < zSize; i++)
        {
            GameObject fence = Instantiate(UnityEngine.Random.Range(0, 2) == 0 ? fencePref1 : fencePref2, fenceParent.transform);
            fence.transform.position = new Vector3(-1, 1, i);
            fence.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
            fence.transform.Rotate(0f, 1f, 0f, 0f);
        }

        //top
        for (int i = 0; i < xSize; i++)
        {
            GameObject fence = Instantiate(UnityEngine.Random.Range(0, 2) == 0 ? fencePref1 : fencePref2, fenceParent.transform);
            fence.transform.position = new Vector3(i, 1, zSize);
            fence.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
            fence.transform.Rotate(0f, 90f, 0f, 0f);
        }

        //right
        for (int i = 0; i < zSize; i++)
        {
            GameObject fence = Instantiate(UnityEngine.Random.Range(0, 2) == 0 ? fencePref1 : fencePref2, fenceParent.transform);
            fence.transform.position = new Vector3(xSize, 1, i);
            fence.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
            fence.transform.Rotate(0f, -180f, 0f, 0f);
        }

        /*  //edge bottom left
          GameObject cube2 = Instantiate(fenceEdge, fenceParent.transform);
          cube2.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
          cube2.transform.position = new Vector3(-1, 1, -1);
          cube2.transform.rotation = new Quaternion(0, 0, 0, 0);
          cube2.name = cube2.transform.position.ToString();

          //edge bottom right
          cube2 = Instantiate(fenceEdge, frameParent.transform);
          cube2.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
          cube2.transform.position = new Vector3(xSize, 1, -1);
          cube2.transform.rotation = new Quaternion(0, -90, 0, 0);
          cube2.name = cube2.transform.position.ToString();

          //edge top left
          cube2 = Instantiate(fenceEdge, frameParent.transform);
          cube2.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
          cube2.transform.position = new Vector3(-1, 1, zSize);
          cube2.transform.rotation = new Quaternion(0, 90, 0, 0);
          cube2.name = cube2.transform.position.ToString();

          //edge top right
          cube2 = Instantiate(fenceEdge, frameParent.transform);
          cube2.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
          cube2.transform.position = new Vector3(xSize, 1, zSize);
          cube2.transform.rotation = new Quaternion(0, 90, 0, 0);
          cube2.name = cube2.transform.position.ToString();*/
    }

    public bool[,] GetRevealedBool()
    {
        bool[,] revealedMap = new bool[xSize, zSize];
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                revealedMap[i, j] = tiles[i, j].GetComponent<Tile>().Revealed;
            }
        }

        return revealedMap;
    }

    public bool[,] GetFlaggedMap()
    {
        bool[,] flaggedMap = new bool[xSize, zSize];
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                flaggedMap[i, j] = tiles[i, j].GetComponent<Tile>().isFlagged;
            }
        }

        return flaggedMap;
    }
}
