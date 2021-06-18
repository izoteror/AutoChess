using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LvlActivate
{
    public static List<Level> LvlButtons  = new List<Level>();
    public static void ActivateLvlMethod(int number)
    {
        for(int i= 0; i < number;i++)
        {
            LvlButtons[i].LevelDone();
        }
        LvlButtons[number].Activate();
    }
}
