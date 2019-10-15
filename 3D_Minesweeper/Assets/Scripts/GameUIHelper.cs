using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIHelper : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeCount;
    [SerializeField]
    TextMeshProUGUI flagCount;
    [SerializeField]
    MapGenerations mapGenerations;
    [SerializeField]
    GameObject pauseUI;
    [SerializeField]
    GameObject tipsPanel;
    [SerializeField]
    TextMeshProUGUI tipsText;

    [HideInInspector]
    public static bool StopTime = false;
    public static int playTime = 0;

    public bool showTips = true;

    // Start is called before the first frame update
    void Start()
    {
        playTime = 0;
        StopTime = false;
        Invoke("IncreasePlayTime", 1f);

        SetTip("Use WASD or the arow keys to move.\nPress Q to reveal tile.\nPress E to place/remove flag.");
        TipsPanelShowHide(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI.activeSelf)
            {
                pauseUI.GetComponent<PauseUIHelper>().Resume();
            }
            else
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (tipsPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TipsPanelShowHide(false);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                TipsPanelShowHide(false);
                SettingsUiHelper.showTips = false;

            }
        }
    }

    public void SetTimeCount(string txt)
    {
        timeCount.text = txt;
    }

    public void SetFlagCount(string txt)
    {
        flagCount.text = txt;
    }

    void IncreasePlayTime()
    {
        if (!StopTime)
        {
            playTime++;
            SetTimeCount(playTime.ToString());
            Invoke("IncreasePlayTime", 1f);
        }
    }

    public void TipsPanelShowHide(bool show)
    {
        Debug.Log(SettingsUiHelper.showTips);
        if (SettingsUiHelper.showTips)
        {
            tipsPanel.SetActive(show);

            if (show)
            {
                Invoke("HideTipps", 10);
            }
            else
            {
                CancelInvoke();
            }
        }
        else
        {
            CancelInvoke();
            tipsPanel.SetActive(false);
        }
    }

    public void SetTip(string text)
    {
        tipsText.text = text;
    }

    void HideTipps()
    {
        if (tipsPanel.activeSelf)
        {
            tipsPanel.SetActive(false);
        }
    }
}
