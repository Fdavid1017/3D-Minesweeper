using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficulitySelectHelper : MonoBehaviour
{
    public Slider heightSlider;
    public Slider widthSlider;
    public Slider bombsSlider;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DiffToMain();
        }
    }

    public void EasyStart()
    {
        MapGenerations.difficulity = MapGenerations.Difficulity.Easy;
        StartGame();
    }

    public void MediumStart()
    {
        MapGenerations.difficulity = MapGenerations.Difficulity.Medium;
        StartGame();
    }

    public void HardStart()
    {
        MapGenerations.difficulity = MapGenerations.Difficulity.Hard;
        StartGame();
    }

    public void CustomStart()
    {
        MapGenerations.difficulity = MapGenerations.Difficulity.Costum;
        MapGenerations.xSize = Mathf.RoundToInt(widthSlider.GetComponent<Slider>().value);
        MapGenerations.zSize = Mathf.RoundToInt(heightSlider.GetComponent<Slider>().value);
        MapGenerations.bombCount = Mathf.RoundToInt(bombsSlider.GetComponent<Slider>().value);
        StartGame();
    }

    public void HeighSliderChanged()
    {
        bombsSlider.GetComponent<Slider>().maxValue = heightSlider.GetComponent<Slider>().value * widthSlider.GetComponent<Slider>().value;

    }
    public void WidthSliderChanged()
    {
        bombsSlider.GetComponent<Slider>().maxValue = heightSlider.GetComponent<Slider>().value * widthSlider.GetComponent<Slider>().value;

    }

    void StartGame()
    {
        MapGenerations.LoadMap = false;
        SceneManager.LoadScene("SampleScene");
    }

    void DiffToMain()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
