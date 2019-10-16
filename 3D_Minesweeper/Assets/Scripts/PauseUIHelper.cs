using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIHelper : MonoBehaviour
{
    public MapGenerations map;
    public SceneLoader sc;

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        MapGenerations.LoadMap = false;
        sc.LoadLevel("SampleScene");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SaveSystem.SaveMap(map);
        sc.LoadLevel("MainMenu");
    }
}
