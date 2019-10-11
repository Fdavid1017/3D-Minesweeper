using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<Slider>().value.ToString();
    }

    public void ChangeSliderTextValue(GameObject slider)
    {
        transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = slider.GetComponent<Slider>().value.ToString();
    }

    public void IncreaseSlider(Slider slider)
    {
        slider.value++;
    }

    public void DecreaseSlider(Slider slider)
    {
        slider.value--;
    }    
}
