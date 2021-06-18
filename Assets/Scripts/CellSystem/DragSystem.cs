using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DragSystem : MonoBehaviour
{
    private Cell currentCell;
    private GameObject parent;
    private Unit upgradeUnit;
    private Cell upgradeCell;

    public Cell CurrentCell {get => currentCell; set => currentCell = value;}
   
    public void Draging(Unit _unit)
    {
       
        if (_unit.Parent == GetComponent<Handler>().HandField)
        {
            Vector3 mousepos = Input.mousePosition;
            mousepos = new Vector3(mousepos.x, mousepos.y, mousepos.z);
            var _ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            var raycastHit = new RaycastHit();
            if (Physics.Raycast(_ray, out raycastHit))
            {
               
                Cell mouseCell;
                
                if (raycastHit.point.z < -4)
                {
                    if (!Handler.Instance.UnitsInField.Contains(_unit))
                    {
                        mouseCell = HandGrid.Instance.CellFromWorldPosition(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
                        if (mouseCell.Walkable || _unit.transform.position == mouseCell.WorldPosition + mouseCell.Size / 2 )
                        {
                            _unit.transform.position = mouseCell.WorldPosition + mouseCell.Size / 2;
                            CurrentCell = mouseCell;
                            CurrentCell.Walkable = true;
                            parent = Handler.Instance.HandField;
                            OnHand?.Invoke(_unit);
  
                                ShiningToUpgrade(upgradeUnit, false);
                                ShiningToUpgrade(_unit, false);
                                upgradeUnit = null;
                            
                        }
                        else if(!mouseCell.Walkable && GetComponent<UpgradeSystem>().CheckUpgrading(_unit,mouseCell.UnitOnMe))
                        {
                            
                            upgradeUnit = mouseCell.UnitOnMe;
                            upgradeCell = mouseCell;
                            ShiningToUpgrade(upgradeUnit, true);
                            ShiningToUpgrade(_unit, true);
                        }
                        
                       
                    }
                }
                else
                {
                    mouseCell = GridField.Instance.CellFromWorldPosition(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));

                    if (mouseCell.Y < GridField.Instance.Height / 2 && mouseCell.Walkable || _unit.transform.position == mouseCell.WorldPosition + mouseCell.Size / 2)
                    {
                        _unit.transform.position = mouseCell.WorldPosition + mouseCell.Size / 2;                      
                        ShiningToUpgrade(upgradeUnit, false);                
                        ShiningToUpgrade(_unit, false);
                        upgradeUnit = null;    
                        CurrentCell.UnitOnMe = null;
                        CurrentCell = mouseCell;
                        CurrentCell.Walkable = true;
                        parent = Handler.Instance.BattleField;
                        OnBattleField?.Invoke(_unit);
                    }
                }
            }
        }
 
    }

    private void ShiningToUpgrade(Unit _unit, bool _active)
    {
        Unit shiningUnit = null;
        if (_active &&  _unit)
        {
            _unit.GetComponent<UIUnit>().ShineOnUpgrade();
            
            shiningUnit = _unit;
        }
        else if (!_active && _unit)
        {
            _unit.GetComponent<UIUnit>().ShineOffUpgrade();
        }
    }

    public event UnityAction<Unit> OnBattleField;
    public event UnityAction<Unit> OnHand;
    public void LocateUnit(Unit _unit)
    {
        ShiningToUpgrade(_unit, false);
        ShiningToUpgrade(upgradeUnit, false);
        if (_unit.Parent == GetComponent<Handler>().HandField)
        {
            _unit.transform.position = CurrentCell.WorldPosition + CurrentCell.Size / 2;
            CurrentCell.Walkable = false;
            _unit.Parent = parent;
        }
        
        if (parent == Handler.Instance.BattleField)
        {

            MatchControl.Instant.ResetTargets(_unit);
            _unit.GetComponent<FigurePresenter>().Mine(false);
            _unit.Find();
           
        }
        else if (parent == Handler.Instance.HandField)
        {
            CurrentCell.UnitOnMe = _unit;
        }

        if (upgradeUnit)
        {
            GetComponent<UpgradeSystem>().TryToUpgrade(upgradeUnit, _unit,upgradeCell);
        }
       
    }
}
