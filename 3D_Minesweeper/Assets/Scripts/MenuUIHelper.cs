using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuUIHelper : MonoBehaviour
{
    [SerializeField]
    GameObject diffSelect;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject settingsUI;
    [SerializeField]
    GameObject infoPanel;
    [SerializeField]
    TextMeshProUGUI infoText;
    [SerializeField]
    GameObject widthSlider;
    [SerializeField]
    GameObject heightSlider;
    [SerializeField]
    GameObject bombCountSlider;
    [SerializeField]
    GameObject mapInfoPanel;
    [SerializeField]
    Button loadButton;

    int dif = 0;
    MapData mapData;

    private void Start()
    {
        mapData = SaveSystem.LoadMap();
        if (mapData == null)
        {
            loadButton.interactable = false;
        }
    }


    public void PlayAgain()
    {
        MapGenerations.LoadMap = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenDiffSelect()
    {
        mainMenu.SetActive(false);
        diffSelect.SetActive(true);
    }

    public void LoadGame()
    {
        MapGenerations.data = mapData;
        MapGenerations.LoadMap = true;
        SceneManager.LoadScene("SampleScene");
    }

    public void HighScores()
    {
        Debug.Log("High scores");
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("App quit");
        Application.Quit();
    }

    public void ShowLoadInfo()
    {
        ChangeInfoText(mapData == null ? "No save file found!" : mapData.mapX + " x " + mapData.mapZ + "\n" + mapData.bombCount + " Bomb\n"
            + mapData.revealedCount + " Revealed");
        infoPanel.SetActive(true);
    }

    public void HideLoadInfo()
    {
        infoPanel.SetActive(false);
    }

    public void ChangeInfoText(string text)
    {
        infoText.text = text;
    }
}
