using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUpgradeSystem : MonoBehaviour
{
    [SerializeField] GameObject UpgSlime;
    [SerializeField] GameObject UpgTurtle;
    [SerializeField] GameObject UpgKnight;

    private void Start()
    {
        UpgSlime.GetComponent<UnitUpgradeUI>().UpdateUI(UnitsStats.slime);
        UpgTurtle.GetComponent<UnitUpgradeUI>().UpdateUI(UnitsStats.turtle);
        UpgKnight.GetComponent<UnitUpgradeUI>().UpdateUI(UnitsStats.knight);
        SlimeChange();
    }

    public void SlimeUpgr()
    {
        float slimePrice =UnitsStats.slime.Price;
        if (GlobalMoneyCount.GlobalMoney > slimePrice)
        {
            GlobalMoneyCount.GlobalMoney -= slimePrice;
            UnitsStats.slime.Lvl++;
            SlimeChange();


        }
    }

    private void SlimeChange()
    {
         UnitsStats.slime.Health = Mathf.RoundToInt(UnitsStats.slime.Health * 1.1f* UnitsStats.slime.Lvl);
            UnitsStats.slime.Atack *= 1.1f *UnitsStats.slime.Lvl;
            //UnitsStats.slime.AtackSpeed *= value;
            UnitsStats.slime.Speed *= 1.1f *UnitsStats.slime.Lvl;     
            UnitsStats.slime.MoneyMining *= 1.1f * UnitsStats.slime.Lvl;
            UnitsStats.slime.Price *= 1.1f * UnitsStats.slime.Lvl;
        UpgSlime.GetComponent<UnitUpgradeUI>().UpdateUI(UnitsStats.slime);
    }

    public void TurtleUpgr()
    {
        float turtlePrice = UnitsStats.turtle.Price* UnitsStats.turtle.Lvl;
        if (GlobalMoneyCount.GlobalMoney > turtlePrice)
        {
            GlobalMoneyCount.GlobalMoney -= turtlePrice;
            UnitsStats.turtle.Health = Mathf.RoundToInt(UnitsStats.turtle.Health * 1.1f);
            UnitsStats.turtle.Atack *= 1.1f;
            // UnitsStats.turtle.AtackSpeed *= value;
            UnitsStats.turtle.Speed *= 1.1f;
            UnitsStats.turtle.Lvl++;
            UnitsStats.turtle.MoneyMining *= 1.1f;
            UpgTurtle.GetComponent<UnitUpgradeUI>().UpdateUI(UnitsStats.turtle);
        }
    }

    public void KnightUpgr()
    {
        float knightPrice =  UnitsStats.knight.Price* UnitsStats.knight.Lvl;
        if (GlobalMoneyCount.GlobalMoney > knightPrice)
        {
            GlobalMoneyCount.GlobalMoney -= knightPrice;
            UnitsStats.knight.Health = Mathf.RoundToInt(UnitsStats.knight.Health * 1.1f);
            UnitsStats.knight.Atack *= 1.1f;
            //UnitsStats.slime.AtackSpeed *= value;
            UnitsStats.knight.Speed *= 1.1f;
            UnitsStats.knight.Lvl++;
            UnitsStats.knight.MoneyMining *= 1.1f;
            UpgKnight.GetComponent<UnitUpgradeUI>().UpdateUI(UnitsStats.knight);
        }
    }
}
