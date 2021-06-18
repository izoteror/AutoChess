using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    public GameObject BattleField;
    public GameObject HandField;
    public MatchControl Match;
    public Cell CurrentCell;
    public int MoneyIncome;
    [SerializeField] Gamer _enemy;
    [SerializeField] public GameObject[] Unitprefab;
    
    public List<Unit> UnitsInHand = new List<Unit>();
    public List<Unit> UnitsInField = new List<Unit>();
    public static Handler Instance;
    public GameObject CreatingSphere;
    [SerializeField] private HandGrid handGrid;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CreateUnit(GameObject unitprefab, Cell positionCell, int lvl)
    {
        Cell avalibleCell=null;
        if (positionCell.Walkable)
        {
            avalibleCell = positionCell;
            
        }
        int moneycount = GetComponent<MoneySystem>().Money;
        Unit currentUnit = unitprefab.GetComponent<Unit>();
        if (moneycount >= currentUnit.Price)
        {
            if (avalibleCell == null)
            {
                for (int i = 0; i < HandGrid.Instance.Grid.GetLength(0); i++)
                {
                    if (HandGrid.Instance.Grid[i, 0].Walkable)
                    {

                        avalibleCell = HandGrid.Instance.Grid[i, 0];
                        return;

                    }

                }
            }
            GetComponent<MoneySystem>().Money -= currentUnit.Price;
            StartCoroutine(Creating(currentUnit.TimeToCreate, avalibleCell, unitprefab,lvl));
           
            //Match.ResetTargets(unit.GetComponent<Unit>());
        }
    }

   IEnumerator Creating(float wait, Cell avalibleCell, GameObject unitprefab, int lvl)
    {
        avalibleCell.Walkable = false;
        GameObject creating = Instantiate(CreatingSphere, avalibleCell.WorldPosition + avalibleCell.Size / 2 +new Vector3(0,0.2f,0), Quaternion.identity);
        ParticleSystem ps = creating.GetComponent<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.duration = wait;
        main.startLifetime = wait;
        ps.Play();

        yield return new WaitForSeconds(wait);
        Destroy(creating);
        GameObject unit = Instantiate(unitprefab, avalibleCell.WorldPosition + avalibleCell.Size / 2, Quaternion.identity);
        avalibleCell.UnitOnMe = unit.GetComponent<Unit>();
        
        AddUnitToHand(unit.GetComponent<Unit>());
        unit.GetComponent<Unit>().UnitSetup(_enemy,HandField,GetComponent<DragSystem>(),lvl,true,0);
        unit.GetComponent<FigurePresenter>().Activated(true);
    }

    public void Buy(int prefabID)
    {
        Cell avalibleCell;
        for (int i = 0; i < HandGrid.Instance.Grid.GetLength(0); i++)
        {
            if (HandGrid.Instance.Grid[i, 0].Walkable)
            {
                avalibleCell = HandGrid.Instance.Grid[i, 0];
                Cell currentCell = avalibleCell;
                CreateUnit(Unitprefab[prefabID], currentCell, 0);
                
                return;

            }
           
        }
        
    }

    public void AddUnitToHand(Unit Unit)
    {
        UnitsInHand.Add(Unit);
        GetComponent<DragSystem>().OnBattleField += FromHandToField;
    }

    private void FromHandToField(Unit Unit)
    {
        if (UnitsInHand.Contains(Unit))
        {
            UnitsInHand.Remove(Unit);
            if(!UnitsInField.Contains(Unit))
            UnitsInField.Add(Unit);
           
        }
    }
}
