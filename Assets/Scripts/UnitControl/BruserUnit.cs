using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BruserUnit : Unit
{
    // Start is called before the first frame update
    public BruserUnit(Vector3 pos, Unit[] Units)
    {
        gameObject.transform.position = pos;
        timer = true;

    }
    public override event UnityAction<Unit> OnDeath;
    public override void UnitStats()
    {
        if (Team == 0)
        {
            health = SceneDataTranslator.KnightT1.Health;
            Atack = SceneDataTranslator.KnightT1.Atack;
            AtackSpeed = SceneDataTranslator.KnightT1.AtackSpeed;
            Speed = SceneDataTranslator.KnightT1.Speed;
        }
        if (Team == 1)
        {
            health = SceneDataTranslator.KnightT2.Health;
            Atack = SceneDataTranslator.KnightT2.Atack;
            AtackSpeed = SceneDataTranslator.KnightT2.AtackSpeed;
            Speed = SceneDataTranslator.KnightT2.Speed;
        }
    }
    public override void Find()
    {
        List<Unit> Units;
        if (Team == 1)
        {
            Units = MatchControl.Instant.Units;
        }
        else
        {
            Units = MatchControl.Instant.AIUnits;
        }
        _target = GetTarget(Units);
        if (_target)
        {
            GetComponent<Movement>().MoveTo(_target.gameObject, _atackdistance);

        }

    }

    override public void Death()
    {
        if (this.Team == 1)
            GlobalMoneyCount.GlobalMoney += (SceneDataTranslator.LvlNumber * UnitsStats.knight.Price)/5;
       
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
