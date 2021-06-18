using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalMoneyCount
{
    // Start is called before the first frame update
    static double  globalMoney;

    static public double GlobalMoney { get => globalMoney; set => globalMoney = value; }

   
}
