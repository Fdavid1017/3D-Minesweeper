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
        Cursor.visible = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        MapGenerations.LoadMap = false;
        sc.LoadLevel("SampleScene");
    }

    public void MainMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        SaveSystem.SaveMap(map);
        sc.LoadLevel("MainMenu");
    }
}
