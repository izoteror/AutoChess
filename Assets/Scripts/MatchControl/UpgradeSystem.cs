using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public Unit[] FirstUnitUpgrade;
    public Unit[] SecondUnitUpgrade;
    public Unit[] ThirdUnitUpgrade;
 
   
    private Unit[] ChooseUpgradingUnit(Unit unitwhoupgrade)
    {
       switch( unitwhoupgrade.TypeOfUnit)
        {
            case 0:
               return FirstUnitUpgrade;

            case 1:
                return SecondUnitUpgrade;
               
            case 2:
                return ThirdUnitUpgrade;
            default:
                return null;
               
        }
        
    }

    public void TryToUpgrade(Unit unitwhoupgrade, Unit upgradableUnit, Cell upgradeCell)
    {
        upgradableUnit.Upgraded();
        unitwhoupgrade.Upgraded();
        Unit[] upgradingUnit = ChooseUpgradingUnit(unitwhoupgrade);
        GetComponent<Handler>().CreateUnit(upgradingUnit[unitwhoupgrade.LvlOfUnit + 1].gameObject, upgradeCell, upgradableUnit.LvlOfUnit + 1);
    }
    public bool CheckUpgrading(Unit unitwhoupgrade, Unit upgradableUnit)
    {
        Unit[] upgradingUnits = ChooseUpgradingUnit(unitwhoupgrade);
        if (upgradingUnits.Length> unitwhoupgrade.LvlOfUnit+1 && upgradableUnit.TypeOfUnit == unitwhoupgrade.TypeOfUnit && upgradableUnit.LvlOfUnit == unitwhoupgrade.LvlOfUnit && GetComponent<MoneySystem>().Money >= upgradingUnits[upgradableUnit.LvlOfUnit+1].GetComponent<Unit>().Price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
