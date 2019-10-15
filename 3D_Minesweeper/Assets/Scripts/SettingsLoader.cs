using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    public SettingsUiHelper settings;

    // Start is called before the first frame update
    void Start()
    {
        SettingsData data = SaveSystem.LoadSettings();
        if (data != null)
        {
            //Set the values
            Debug.Log(data.resolutionWidth + " x " + data.resolutionHeight + " " + data.fullScreen);
            Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, data.fullScreen);
            settings.fullscreenButton.SetState(data.fullScreen);
            QualitySettings.SetQualityLevel(data.graphicsQuality);
            settings.musicAudio.SetFloat("volume", data.musicVolume);
            settings.fxaudio.SetFloat("volume", data.sfxVolume);
            SettingsUiHelper.showTips = data.showTips;

            //Set the UI
            settings.ChangeResolutionText(Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString());
            settings.ChangeGraphicsText(QualitySettings.names[QualitySettings.GetQualityLevel()]);
            float temp = 0f;
            settings.musicAudio.GetFloat("volume", out temp);
            settings.VolumeSliderShowValue(temp);
            settings.volumeSlider.value = temp;
            settings.fxaudio.GetFloat("volume", out temp);
            settings.FXVolumeSliderShowValue(temp);
            settings.sfxSlider.value = temp;
            settings.tipsButton.SetState(SettingsUiHelper.showTips);
        }
        else
        {
            Debug.LogError("Settings file not setted");
        }
    }
}
