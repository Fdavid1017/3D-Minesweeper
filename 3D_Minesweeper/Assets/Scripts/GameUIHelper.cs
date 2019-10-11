using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIHelper : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeCount;
    [SerializeField]
    TextMeshProUGUI flagCount;
    [SerializeField]
    MapGenerations mapGenerations;

    public static int playTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("IncreasePlayTime", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveSystem.SaveMap(mapGenerations);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void SetTimeCount(string txt)
    {
        timeCount.text = txt;
    }

    public void SetFlagCount(string txt)
    {
        flagCount.text = txt;
    }

    void IncreasePlayTime()
    {
        playTime++;
        SetTimeCount(playTime.ToString());
        Invoke("IncreasePlayTime", 1f);
    }
}
