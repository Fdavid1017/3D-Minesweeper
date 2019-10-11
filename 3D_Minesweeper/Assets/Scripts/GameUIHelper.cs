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
    [SerializeField]
    GameObject pauseUI;

    [HideInInspector]
    public static bool StopTime = false;
    public static int playTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("IncreasePlayTime", 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
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
        if (!StopTime)
        {
            playTime++;
            SetTimeCount(playTime.ToString());
            Invoke("IncreasePlayTime", 1f);
        }
    }
}
