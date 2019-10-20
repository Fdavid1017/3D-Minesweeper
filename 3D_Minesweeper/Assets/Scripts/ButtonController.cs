using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Animator anim;

    public void OnHoverEnter()
    {
        anim.SetBool("Hovering", true);
    }

    public void OnHoverExit()
    {
        anim.SetBool("Hovering", false);
    }
}
