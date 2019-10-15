using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public GameUIHelper gameUIHelper;
    public AudioClip dirt;
    public AudioClip stone;
    public AudioSource audio;

    int flagCount;
    MapGenerations mapGenerator;
    private Vector3 moveDirection = Vector3.zero;
    Vector2 previousLocation = new Vector2();
    Vector3 previousLocationRAW = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = GameObject.Find("TileMap").GetComponent<MapGenerations>();
        previousLocation.x = Mathf.RoundToInt(transform.position.x);
        previousLocation.y = Mathf.RoundToInt(transform.position.z);
        previousLocationRAW = transform.position;
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
        if (add)
        {
            mapGenerator.tiles[x, z].GetComponent<Tile>().FlagModell(true);
        }
        else
        {
            mapGenerator.tiles[x, z].GetComponent<Tile>().FlagModell(false);
        }
    }

    void RevealTile(int x, int z)
    {
        if (!(x >= 0 && x <= MapGenerations.xSize && z >= 0 && z <= MapGenerations.zSize))
        {
            return;
        }

        if (MapGenerations.revealedTilesCount < 1)
        {
            mapGenerator.GenerateBombs(x, z);
        }

        if (mapGenerator.tiles[x, z].GetComponent<Tile>().NearbyCount == 0)
        {
            mapGenerator.RevealNearbyTiles(x, z);
        }
        else
        {
            mapGenerator.tiles[x, z].GetComponent<Tile>().Revealed = true;

            if (mapGenerator.tiles[x, z].GetComponent<Tile>().NearbyCount != -1)
            {
                mapGenerator.IncreaseRevelaedTileCount(1);
            }
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

    public void StepSound()
    {
        if (transform.position != previousLocationRAW)
        {

            int x = Mathf.RoundToInt(transform.position.x);
            int z = Mathf.RoundToInt(transform.position.z);
            audio.clip = null;
            if (mapGenerator.tiles[x, z].GetComponent<Tile>().Revealed)
            {
                audio.clip = dirt;
            }
            else
            {
                audio.clip = stone;
            }
            audio.Play();
            previousLocationRAW = transform.position;
        }
    }
}
