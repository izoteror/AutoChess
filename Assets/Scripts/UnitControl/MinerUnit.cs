using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinerUnit : Unit
{
    public MinerUnit(Vector3 pos, Unit[] Units)
    {
        gameObject.transform.position = pos;


    }
    public override event UnityAction<Unit> OnDeath;

    public override void UnitStats()
    {
        if (Team == 0)
        {
            health = SceneDataTranslator.SlimeT1.Health;
            Atack = SceneDataTranslator.SlimeT1.Atack;
            AtackSpeed = SceneDataTranslator.SlimeT1.AtackSpeed;
            Speed = SceneDataTranslator.SlimeT1.Speed;
        }
        if (Team == 1)
        {
            health = SceneDataTranslator.SlimeT2.Health;
            Atack = SceneDataTranslator.SlimeT2.Atack;
            AtackSpeed = SceneDataTranslator.SlimeT2.AtackSpeed;
            Speed = SceneDataTranslator.SlimeT2.Speed;
        }
    }
    public override void Find()
    {
        GetComponent<FigurePresenter>().Happy(0);
    }

     override public void Death()
    {
        if (this.Team == 1)
        {
            GlobalMoneyCount.GlobalMoney += SceneDataTranslator.LvlNumber * UnitsStats.slime.Price / 5;

        }
       
        OnDeath?.Invoke(this);

        Cell deathCell;
        if (Parent == Handler.Instance.HandField)
        {
            deathCell = HandGrid.Instance.CellFromWorldPosition(this.transform.position);
            Handler.Instance.UnitsInHand.Remove(this);

        }
        else
        {
            deathCell = GridField.Instance.CellFromWorldPosition(this.transform.position);
            Handler.Instance.UnitsInField.Remove(this);
        }
        deathCell.Walkable = true;

    }
}
