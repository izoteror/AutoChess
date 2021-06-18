using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeaderInfo : MonoBehaviour
{
    [SerializeField] Text globalMoneyCount;
    [SerializeField] Text globalLvl;
    // Update is called once per frame
    void FixedUpdate()
    {
        globalMoneyCount.text = GlobalMoneyCount.GlobalMoney.ToString();
        globalLvl.text = (SceneDataTranslator.LvlNumber).ToString();
    }
}
