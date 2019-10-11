using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalHelper
{
    public static void OrderHighscoresAsc(ref List<Highscore> highscores)
    {
        int n = highscores.Count;
        int i, j;
        Highscore temp;
        bool swapped;
        for (i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (j = 0; j < n - i - 1; j++)
            {
                if (highscores[j].time > highscores[j + 1].time)
                {
                    temp = highscores[j];
                    highscores[j] = highscores[j + 1];
                    highscores[j + 1] = temp;
                    swapped = true;
                }
            }

            if (swapped == false)
                break;
        }
    }
}
