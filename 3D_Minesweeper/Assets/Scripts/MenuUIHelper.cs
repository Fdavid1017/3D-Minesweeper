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

    void ChangeDifficulity()
    {
        widthSlider.SetActive(false);
        heightSlider.SetActive(false);
        bombCountSlider.SetActive(false);
        switch (dif)
        {
            case 0:
                MapGenerations.difficulity = MapGenerations.Difficulity.Easy;
                mapInfoPanel.SetActive(true);
                mapInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "8 x 8\n10 Bomb";
                break;
            case 1:
                MapGenerations.difficulity = MapGenerations.Difficulity.Medium;
                mapInfoPanel.SetActive(true);
                mapInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "16 x 16\n40 Bomb";
                break;
            case 2:
                MapGenerations.difficulity = MapGenerations.Difficulity.Hard;
                mapInfoPanel.SetActive(true);
                mapInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "16 x 30\n99 Bomb";
                break;
            case 3:
                mapInfoPanel.SetActive(false);
                MapGenerations.difficulity = MapGenerations.Difficulity.Costum;
                widthSlider.SetActive(true);
                heightSlider.SetActive(true);
                bombCountSlider.SetActive(true);
                bombCountSlider.GetComponent<Slider>().maxValue = heightSlider.GetComponent<Slider>().value * widthSlider.GetComponent<Slider>().value;
                break;
            default:
                Debug.LogError("Difficulity int must be between 0 and 3!!!");
                break;
        }

        GameObject.Find("DifficulityText").GetComponent<TextMeshProUGUI>().text = MapGenerations.difficulity.ToString();
    }

    public void IncreaseDifficulity()
    {
        dif = dif < 3 ? dif + 1 : 0;
        ChangeDifficulity();
    }

    public void DecreaseDifficulity()
    {
        dif = dif > 0 ? dif - 1 : 3;
        ChangeDifficulity();
    }

    public void HeightSliderChanged(Slider slider)
    {
        int value = Mathf.RoundToInt(slider.value);
        if (value > 50)
        {
            value = 50;
        }
        else if (value < 1)
        {
            value = 1;
        }
        MapGenerations.xSize = value;
        bombCountSlider.GetComponent<Slider>().maxValue = slider.value * widthSlider.GetComponent<Slider>().value;
    }

    public void WidthSliderChanged(Slider slider)
    {
        int value = Mathf.RoundToInt(slider.value);
        if (value > 100)
        {
            value = 100;
        }
        else if (value < 1)
        {
            value = 1;
        }
        MapGenerations.zSize = value;
        bombCountSlider.GetComponent<Slider>().maxValue = heightSlider.GetComponent<Slider>().value * slider.value;
    }

    public void BombCountSliderChanged(Slider slider)
    {
        int value = Mathf.RoundToInt(slider.value);
        if (value > (MapGenerations.xSize * MapGenerations.zSize))
        {
            value = MapGenerations.xSize * MapGenerations.zSize;
        }
        else if (value < 1)
        {
            value = 1;
        }
        MapGenerations.bombCount = value;
    }

    public void DiffToMain()
    {
        diffSelect.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("x: " + MapGenerations.xSize + "\tz: " + MapGenerations.zSize + "\tBombs: " + MapGenerations.bombCount);
        MapGenerations.LoadMap = false;
        SceneManager.LoadScene("SampleScene");
    }
}
