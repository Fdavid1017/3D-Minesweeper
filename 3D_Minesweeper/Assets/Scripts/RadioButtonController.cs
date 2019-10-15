using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioButtonController : MonoBehaviour
{
    public SVGImage imageEditor;
    public Sprite onImage;
    public Sprite offImage;
    public bool on = false;

    private void Start()
    {
        if (on)
        {
            imageEditor.sprite = onImage;
        }
        else
        {
            imageEditor.sprite = offImage;
        }
    }

    public void ChangeState()
    {
        on = !on;
        if (on)
        {
            imageEditor.sprite = onImage;
        }
        else
        {
            imageEditor.sprite = offImage;
        }
    }

    public void OnHoverEnter()
    {
        imageEditor.color = Color.gray;
    }

    public void OnHoverExit()
    {
        imageEditor.color = Color.white;
    }

    public void SetState(bool newState)
    {
        on = newState;
        if (on)
        {
            imageEditor.sprite = onImage;
        }
        else
        {
            imageEditor.sprite = offImage;
        }
    }
}
