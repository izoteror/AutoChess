using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoad
{
    public static void Load()
    {
        SceneDataTranslator.LvlNumber = PlayerPrefs.GetInt("Lvl");
        GlobalMoneyCount.GlobalMoney = PlayerPrefs.GetInt("Money");
        UnitsStats.slime.Lvl = PlayerPrefs.GetInt("SlimeLvl");
        UnitsStats.turtle.Lvl = PlayerPrefs.GetInt("TurtleLvl");
        UnitsStats.knight.Lvl = PlayerPrefs.GetInt("KnightLvl");
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Lvl", SceneDataTranslator.LvlNumber);
        PlayerPrefs.SetInt("Money", (int)GlobalMoneyCount.GlobalMoney);
        PlayerPrefs.SetInt("SlimeLvl", UnitsStats.slime.Lvl);
        PlayerPrefs.SetInt("TurtleLvl", UnitsStats.turtle.Lvl);
        PlayerPrefs.SetInt("KnightLvl", UnitsStats.knight.Lvl);
    }
   
}
