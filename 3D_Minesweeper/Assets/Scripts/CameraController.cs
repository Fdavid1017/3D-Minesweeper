using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        float time = UnityEngine.Random.Range(0, 5);
        Invoke("playBirdSounds", time);
    }

    private void playBirdSounds()
    {

        if (!audio.isPlaying)
        {
            audio.Play();
        }

        float time = UnityEngine.Random.Range(0, 5);
        Invoke("playBirdSounds", time);
    }
}
