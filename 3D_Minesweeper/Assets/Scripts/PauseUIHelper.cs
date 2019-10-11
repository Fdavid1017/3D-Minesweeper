using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIHelper : MonoBehaviour
{
    public MapGenerations map;

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        MapGenerations.LoadMap = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SaveSystem.SaveMap(map);
        SceneManager.LoadScene("MainMenu");
    }
}
