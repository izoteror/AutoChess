using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeUI : MonoBehaviour
{

    [SerializeField] Text nameText;
    [SerializeField] Text healthText;
    [SerializeField] Text atackText;
    [SerializeField] Text atackspeedText;
    [SerializeField] Text lvlText;
    [SerializeField] Text speedText;
    [SerializeField] Text moneyMine;
    [SerializeField] Text priceText;
    [SerializeField] int IDUnit;

    

    private void Start()
    {
        if (IDUnit == 0)
        {
            
            nameText.text = "Slime";
        }
        if (IDUnit == 1)
        {
           
            nameText.text = "Turtle";
        }
        if (IDUnit == 2)
        {

            nameText.text = "Knight";
        }
    }

    public void UpdateUI(StatsStruct current)
    {
        atackText.text = "Atack: " + current.Atack.ToString();
        moneyMine.text = "Mining: " + current.MoneyMining.ToString();
        healthText.text = "Health: " + current.Health.ToString();
        lvlText.text = "Lvl: " + current.Lvl.ToString();
        priceText.text = "Price: " +  current.Price;
    }
}
