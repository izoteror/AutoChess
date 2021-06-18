using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public abstract class Unit : MonoBehaviour , IFinder
{
    public GameObject Parent;
    public bool Activate = false;
    public int TypeOfUnit;
    public int LvlOfUnit;
    public Gamer EnemyGamer;
    public Grid Level;
    public int MoneyMining;
    public int Price;
    public float TimeToCreate;
    public float AnimSpeed;
    public float health;
    public float Atack;
    public float Speed;
    public float AtackSpeed;
    private List<Unit> enemies;
    [SerializeField] protected Unit _target;
    private Transform _selfTransform;
    private Vector3Int _cellPosition;
    private Vector3 _pos;
    private bool _isAtacked = false;
    [SerializeField] protected bool timer = true;
    [SerializeField] protected int _atackdistance;
    [SerializeField] private int _team;
    public GameObject HandlerObj;
    

   
  

    public float Health { get => health; set 
        {
            health = value;
           
            GetComponent<UIUnit>().HealthBarChanger();
            if (health<=0)
                Death();
        } }

    public bool IsAtacked { get => _isAtacked; }
    public int Team { get => _team; set => _team = value; }

    public virtual void UnitStats()
    {
      
    }

    public void UnitSetup(Gamer _enemy, GameObject _gameZone, DragSystem _dragSys, int _lvl, bool _activate, int _team)
    {
        EnemyGamer = _enemy;
        Parent = _gameZone;
        GetComponent<Drag>().Drager = _dragSys;
        LvlOfUnit = _lvl;
        Activate = _activate;
        Team = _team;
        UnitStats();

    }

    public virtual void Find()
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
        _target =  GetTarget(Units);
        if (_target)
        {
            GetComponent<Movement>().MoveTo(_target.gameObject, _atackdistance);
            
        }
       
   
        
        
              
    }

   /* public Unit(Vector3 pos, Unit[] Units)
    {
        gameObject.transform.position = pos; 
        
    }*/


    

    public Unit GetTarget(List<Unit> Units)
    {
        if (GridField.Instance)
        {
            PathFinding pathFinding = GridField.Instance.gameObject.GetComponent<PathFinding>();
            GridField grid = GridField.Instance;

            List<Unit> enemies = Units;
            if (enemies.Count > 0)
            {
               
                int distance = grid.Width*1000;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i])
                    {
                        int localdistance = pathFinding.GetDistance( grid.CellFromWorldPosition(transform.position), grid.CellFromWorldPosition(enemies[i].transform.position));

                        if (distance >= localdistance && enemies[i] != this)
                        {
                            distance = localdistance;
                            _target = enemies[i];
                        }
                        
                    }
                }
                GetComponent<FigurePresenter>().Happy(enemies.Count);
                return _target;
            }
            else if(enemies.Count<=0)
            {
                
                if (!_target && Parent == Handler.Instance.BattleField)
                {
                    Cell currentCell = GridField.Instance.CellFromWorldPosition(this.transform.position);
                    transform.position = currentCell.WorldPosition + currentCell.Size / 2;
                    
                    if (timer)
                    {
                        GetComponent<FigurePresenter>().Happy(0);
                        StartCoroutine(AtackReset());
                        timer = false;
                    }
                }
            }
        }
        
        return null;
    }

    public abstract event UnityAction<Unit> OnDeath;
    
    IEnumerator AtackReset()
    {
        Debug.Log(this.name + " Reset Atack");
        yield return new WaitForSeconds(AtackSpeed);
        
        EnemyGamer.HealthDamage((int)Atack);
        timer = true;
        this.Find();
        
    }

   

    IEnumerator Fight(Unit target)
    {
        while (target)
        {
        
                _isAtacked = true;
                target.Health -= Atack;
                GetComponent<FigurePresenter>().Atack(true, AtackSpeed);
                yield return new WaitForSeconds(AtackSpeed);
        
            
        }
        GetComponent<FigurePresenter>().Atack(false, AtackSpeed);
        _isAtacked = false;
       this.Find();
        
    }

    public void ToAtack(GameObject target)
    {
        if (!_isAtacked)
            StartCoroutine(Fight(target.GetComponent<Unit>()));
        else 
        {
            
            StopCoroutine(Fight(target.GetComponent<Unit>()));
            _isAtacked = false;
           
        }
    }


    public void Upgraded()
    {
        Cell deathCell;
        if (Parent == Handler.Instance.HandField)
        {
            deathCell = HandGrid.Instance.CellFromWorldPosition(this.transform.position);
            Handler.Instance.UnitsInHand.Remove(this);
            deathCell.Walkable = true;
            Destroy(gameObject);
        }
        
    }

    public abstract void Death();

    
}
