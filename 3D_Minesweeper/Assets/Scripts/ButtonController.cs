using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public AudioSource audio;

   public void OnHoverEnter()
    {
        audio.Play();
    }
}
