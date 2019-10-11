using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Highscore
{
    public string name;
    public int time;

    public Highscore(string name, int time)
    {
        this.name = name;
        this.time = time;
    }
}
