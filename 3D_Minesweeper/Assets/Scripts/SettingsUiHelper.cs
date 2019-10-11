using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUiHelper : MonoBehaviour
{
    public GameObject mainMenuUi;
    public GameObject infoPanel;

    public enum Graphics
    {
        Low, Medium, High
    }

    public void ShowInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public void SettingsToMain()
    {
        HideInfoPanel();
        mainMenuUi.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ApplySettings()
    {
        Debug.Log("Settings Applied");
    }
}
