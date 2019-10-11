using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject flagPref;
    public Animator anim;
    public GameUIHelper gameUIHelper;

    Directions dir = Directions.Still;
    int flagCount;
    MapGenerations mapGenerator;
    private Vector3 moveDirection = Vector3.zero;
    Vector2 previousLocation = new Vector2();
    Vector3 flagSpawnLocalPosition = new Vector3(0f, 1f, 0f);
    enum Directions
    {
        North, South, West, East, Still
    }

    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = GameObject.Find("TileMap").GetComponent<MapGenerations>();
        previousLocation.x = Mathf.RoundToInt(transform.position.x);
        previousLocation.y = Mathf.RoundToInt(transform.position.z);
        flagCount = MapGenerations.bombCount;
        gameUIHelper.SetFlagCount(flagCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int z = Mathf.RoundToInt(transform.position.z);

        if (x >= 0 && x <= MapGenerations.xSize && z >= 0 && z <= MapGenerations.zSize)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!mapGenerator.tiles[x, z].GetComponent<Tile>().Revealed)
                {
                    if (flagCount > 0 && !mapGenerator.tiles[x, z].GetComponent<Tile>().isFlagged)
                    {
                        PlaceFlag(mapGenerator.tiles[x, z].transform, true, x, z);
                    }
                    else
                    {
                        PlaceFlag(mapGenerator.tiles[x, z].transform, false, x, z);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q) && !mapGenerator.tiles[x, z].GetComponent<Tile>().isFlagged && !mapGenerator.tiles[x, z].GetComponent<Tile>().Revealed)
            {
                RevealTile(x, z);
            }
        }

        HighLightTile(x, z);
    }

    bool add = false;
    void PlaceFlag(Transform parent, bool addFlag, int x, int z)
    {
        if (!(x >= 0 && x <= MapGenerations.xSize && z >= 0 && z <= MapGenerations.zSize))
        {
            return;
        }

        if (addFlag)
        {
            add = true;
            anim.SetTrigger("Place");
            mapGenerator.tiles[x, z].GetComponent<Tile>().isFlagged = true;
            flagCount--;
        }
        else
        {
            add = false;
            anim.SetTrigger("Pickup");
            Debug.Log("RemoveFlag");
            mapGenerator.tiles[x, z].GetComponent<Tile>().isFlagged = false;
            flagCount++;
        }

        gameUIHelper.SetFlagCount(flagCount.ToString());
        Debug.Log("Flag count: " + flagCount);
    }

    public void FlagModell()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int z = Mathf.RoundToInt(transform.position.z);
        Transform parent = mapGenerator.tiles[x, z].transform;
        if (add)
        {
            GameObject flag = Instantiate(flagPref, parent);
            flag.transform.localPosition = flagSpawnLocalPosition;
        }
        else
        {
            GameObject flag = parent.GetChild(2).gameObject;
            Destroy(flag);
        }
    }

    void RevealTile(int x, int z)
    {
        if (!(x >= 0 && x <= MapGenerations.xSize && z >= 0 && z <= MapGenerations.zSize))
        {
            return;
        }
        if (mapGenerator.tiles[x, z].GetComponent<Tile>().NearbyCount == 0)
        {
            mapGenerator.RevealNearbyTiles(x, z);
        }
        else
        {
            mapGenerator.tiles[x, z].GetComponent<Tile>().Revealed = true;
            MapGenerations.IncreaseRevelaedTileCount(1);
        }
    }

    void HighLightTile(int x, int z)
    {
        if (!(previousLocation.x == x && previousLocation.y == z) && x >= 0 && x <= MapGenerations.xSize && z >= 0 && z <= MapGenerations.zSize)
        {

            if (mapGenerator.tiles[Mathf.RoundToInt(previousLocation.x), Mathf.RoundToInt(previousLocation.y)].gameObject.GetComponent<Tile>().Revealed)
            {
                mapGenerator.tiles[Mathf.RoundToInt(previousLocation.x), Mathf.RoundToInt(previousLocation.y)].gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_9DB77AC7", 0f);
            }
            else
            {
                mapGenerator.tiles[Mathf.RoundToInt(previousLocation.x), Mathf.RoundToInt(previousLocation.y)].gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_9DB77AC7", 0f);
            }

            if (mapGenerator.tiles[x, z].gameObject.GetComponent<Tile>().Revealed)
            {
                mapGenerator.tiles[x, z].gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_9DB77AC7", .5f);
            }
            else
            {
                mapGenerator.tiles[x, z].gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_9DB77AC7", .5f);
            }

            previousLocation.x = x;
            previousLocation.y = z;
        }
    }
}
