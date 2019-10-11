using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsUiHelper : MonoBehaviour
{
    public GameObject mainMenuUi;
    public GameObject infoPanel;
    public TextMeshProUGUI graphicsPickerText;
    public AudioMixer audio;
    public TextMeshProUGUI volumeSliderLabelText;
    public RadioButtonController fullscreenButton;
    public TextMeshProUGUI resolutionText;

    sbyte graphicPosition = 2;
    int resolutionIndex = 0;
    Resolution[] resolutions;
    List<string> resolutionsString = new List<string>();
    bool saved = true;

    private void Start()
    {
        resolutions = Screen.resolutions;
        ChangeResolutionText(Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString());

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionsString.Add(resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString());

            Debug.Log(resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString());

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }

        ChangeGraphicsText(QualitySettings.names[QualitySettings.GetQualityLevel()]);
        volumeSliderLabelText.text = "100";
    }

    public void ApplyResolution()
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Current resoluton is: " + resolution.ToString());
        saved = true;
    }

    public void ResolutionLeft()
    {
        resolutionIndex = resolutionIndex <= 0 ? resolutions.Length - 1 : (sbyte)(resolutionIndex - 1);
        ChangeResolutionText(resolutions[resolutionIndex].width.ToString() + " x " + resolutions[resolutionIndex].height.ToString());
        Debug.Log(resolutions[resolutionIndex].width.ToString() + " x " + resolutions[resolutionIndex].height.ToString());
        saved = false;
    }

    public void ResolutionRight()
    {
        resolutionIndex = resolutionIndex >= resolutions.Length - 1 ? 0 : (sbyte)(resolutionIndex + 1);
        ChangeResolutionText(resolutions[resolutionIndex].width.ToString() + " x " + resolutions[resolutionIndex].height.ToString());
        Debug.Log(resolutions[resolutionIndex].width.ToString() + " x " + resolutions[resolutionIndex].height.ToString());
        saved = false;
    }

    public void ChangeResolutionText(string text)
    {
        resolutionText.text = text;
    }

    public void ShowInfoPanel()
    {
        if (saved)
        {
            SettingsToMain();
        }
        else
        {
            infoPanel.SetActive(true);
        }
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public void SettingsToMain()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }
        ChangeResolutionText(Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString());

        HideInfoPanel();
        mainMenuUi.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ChangeGraphicsText(string text)
    {
        graphicsPickerText.text = text;
    }

    public void GraphicLeft()
    {
        graphicPosition = graphicPosition <= 0 ? (sbyte)2 : (sbyte)(graphicPosition - 1);
        ChangeGraphic(graphicPosition);
    }

    public void GraphicRight()
    {
        graphicPosition = graphicPosition >= 2 ? (sbyte)0 : (sbyte)(graphicPosition + 1);
        ChangeGraphic(graphicPosition);
    }

    void ChangeGraphic(int index)
    {
        QualitySettings.SetQualityLevel(index);
        ChangeGraphicsText(QualitySettings.names[QualitySettings.GetQualityLevel()]);
    }

    public void SetVolume(float volume)
    {
        audio.SetFloat("volume", volume);
        VolumeSliderShowValue(volume);
    }

    void VolumeSliderShowValue(float value)
    {
        volumeSliderLabelText.text = Mathf.RoundToInt(Remap(value, -80, 0, 0, 100)).ToString();
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenButton.on;
    }
}
