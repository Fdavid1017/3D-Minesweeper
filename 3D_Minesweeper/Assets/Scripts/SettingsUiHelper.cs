using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.IO;

public class SettingsUiHelper : MonoBehaviour
{
    public GameObject mainMenuUi;
    public GameObject infoPanel;
    public TextMeshProUGUI graphicsPickerText;
    public AudioMixer fxaudio;
    public AudioMixer musicAudio;
    public TextMeshProUGUI volumeSliderLabelText;
    public TextMeshProUGUI fxVolumeSliderLabelText;
    public RadioButtonController fullscreenButton;
    public TextMeshProUGUI resolutionText;
    public RadioButtonController tipsButton;
    public Slider volumeSlider;
    public Slider sfxSlider;

    public static bool showTips = true;
    static public bool load = false;

    [HideInInspector]
    public sbyte graphicPosition = 2;
    [HideInInspector]
    public int resolutionIndex = 0;
    [HideInInspector]
    public Resolution[] resolutions;
    List<string> resolutionsString = new List<string>();
    bool saved = true;


    private void Start()
    {

        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionsString.Add(resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString());

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }

        if (!File.Exists(SaveSystem.settingsPath))
        {
            ChangeResolutionText(Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString());
            ChangeGraphicsText(QualitySettings.names[QualitySettings.GetQualityLevel()]);
            float temp = 0f;
            musicAudio.GetFloat("volume", out temp);
            VolumeSliderShowValue(temp);
            volumeSlider.value = temp;
            fxaudio.GetFloat("volume", out temp);
            FXVolumeSliderShowValue(temp);
            sfxSlider.value = temp;
            tipsButton.SetState(showTips);
        }
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

        SaveSystem.SaveSettings(this);

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
        musicAudio.SetFloat("volume", volume);
        float a;
        musicAudio.GetFloat("volume", out a);
        Debug.Log(musicAudio.name + "\t" + a);
        VolumeSliderShowValue(volume);
    }

    public void SetFXVolume(float volume)
    {
        fxaudio.SetFloat("volume", volume);
        FXVolumeSliderShowValue(volume);
    }

    public void VolumeSliderShowValue(float value)
    {
        volumeSliderLabelText.text = Mathf.RoundToInt(Remap(value, -80, 0, 0, 100)).ToString();
    }

    public void FXVolumeSliderShowValue(float value)
    {
        fxVolumeSliderLabelText.text = Mathf.RoundToInt(Remap(value, -80, 0, 0, 100)).ToString();
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenButton.on;
    }

    public void SetTips()
    {
        showTips = tipsButton.on;
    }
}
