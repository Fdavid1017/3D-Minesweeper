using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SettingsData
{
    public int resolutionHeight;
    public int resolutionWidth;
    public bool fullScreen;
    public int graphicsQuality;
    public float musicVolume;
    public float sfxVolume;
    public bool showTips;

    public SettingsData(int resolutionHeight, int resolutionWidth, bool fullScreen, int graphicsQuality, int musicVolume, int sfxVolume, bool showTips)
    {
        this.resolutionHeight = resolutionHeight;
        this.resolutionWidth = resolutionWidth;
        this.fullScreen = fullScreen;
        this.graphicsQuality = graphicsQuality;
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
        this.showTips = showTips;
    }

    public SettingsData(SettingsUiHelper settings)
    {
        this.resolutionHeight = Screen.height;
        this.resolutionWidth = Screen.width;
        this.fullScreen = Screen.fullScreen;
        this.graphicsQuality = QualitySettings.GetQualityLevel();
        settings.musicAudio.GetFloat("volume", out this.musicVolume);
        settings.fxaudio.GetFloat("volume", out this.sfxVolume);
        this.showTips = SettingsUiHelper.showTips;
    }
}
