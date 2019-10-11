using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Vector3 size = new Vector3(0.05f, 0.1f, 0.05f);
    private float growSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, size, Time.deltaTime * growSpeed);
    }
}
