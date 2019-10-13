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
    TextMeshProUGUI infoWidth;
    [SerializeField]
    TextMeshProUGUI infoHeight;
    [SerializeField]
    TextMeshProUGUI infoBombCount;
    [SerializeField]
    TextMeshProUGUI infoRevealed;
    [SerializeField]
    TextMeshProUGUI infoMore;
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
        if (mapData == null)
        {
            ChangeInfoText("", "", "", "", "No save file found!");
        }
        else
        {
            ChangeInfoText(mapData.mapX.ToString(), mapData.mapZ.ToString(), mapData.bombCount.ToString(), mapData.revealedCount.ToString(), ""); ;
        }

        infoPanel.SetActive(true);
    }

    public void HideLoadInfo()
    {
        infoPanel.SetActive(false);
    }

    public void ChangeInfoText(string width, string height, string bombCount, string revealed, string moreInfo)
    {
        infoWidth.text = width;
        infoHeight.text = height;
        infoBombCount.text = bombCount;
        infoRevealed.text = revealed;
        infoMore.text = moreInfo
            ;
    }
}
