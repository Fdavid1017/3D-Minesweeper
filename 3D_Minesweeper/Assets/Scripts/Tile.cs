using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    TextMeshPro textMP;
    [SerializeField]
    Material grassMaterial;
    [SerializeField]
    GameObject grassTile;
    [SerializeField]
    float dissolveSpeed = 2f;
    float dissolve = 0;
    float grassTileSize;
    sbyte nearbyCount = 0;
    bool flagged = false;
    bool revealed = false;
    Vector3 flagSpawnLocalPosition = new Vector3(0f, 1f, 0f);

    public GameObject diedScreenUi;
    public GameObject flagPref;


    // Start is called before the first frame update
    void Start()
    {
        textMP.enabled = revealed ? true : false;
        grassTileSize = grassTile.transform.localScale.x;
    }

    private void Update()
    {
        if (revealed)
        {
            if (dissolve < 1)
            {
                dissolve = Mathf.Lerp(dissolve, 1, Time.deltaTime * dissolveSpeed);
                grassTileSize = Mathf.Lerp(grassTileSize, 1, Time.deltaTime * dissolveSpeed * 2);
                gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_7C536670", dissolve);
                grassTile.transform.localScale = new Vector3(grassTileSize, grassTileSize, grassTileSize);
            }
        }
    }

    public bool isFlagged
    {
        get => flagged;
        set { flagged = value; }
    }

    public bool isBomb()
    {
        return nearbyCount == -1;
    }

    public sbyte NearbyCount
    {
        get => nearbyCount;
        set
        {
            nearbyCount = value;
            ChangeText(nearbyCount.ToString());
        }
    }

    public void ChangeText(string text)
    {
        textMP.text = text;
        sbyte number = sbyte.Parse(text);

        switch (number)
        {
            case 0:
                textMP.text = "";
                break;
            case 1:
                textMP.color = Color.blue;
                break;
            case 2:
                textMP.color = Color.green;
                break;
            case 3:
                textMP.color = Color.red;
                break;
            case 4:
                textMP.color = new Color(171, 5, 241);
                break;
            case 5:
                textMP.color = new Color(116, 15, 23);
                break;
            case 6:
                textMP.color = new Color(48, 220, 201);
                break;
            case 7:
                textMP.color = Color.black;
                break;
            case 8:
                textMP.color = Color.grey;
                break;
            default:
                textMP.color = Color.white;
                break;
        }
    }

    public bool Revealed
    {
        get => revealed;
        set
        {
            revealed = value;
            textMP.enabled = revealed ? true : false;

            if (revealed)
            {
                if (nearbyCount == -1)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<SimpleCharacterControl>().isAlive = false;
                    player.transform.position = this.gameObject.transform.position;

                    GameObject.FindGameObjectWithTag("LookFree").SetActive(false);

                    Cursor.visible = true;
                    diedScreenUi.SetActive(true);
                    GameUIHelper.StopTime = true;
                    Destroy(this.gameObject);
                }
            }
        }
    }


    public void FlagModell(bool place)
    {
        if (place)
        {
            GameObject flag = Instantiate(flagPref, transform);
            flag.transform.localPosition = flagSpawnLocalPosition;
        }
        else
        {
            GameObject flag = transform.GetChild(2).gameObject;
            Destroy(flag);
        }
    }
}
