using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _battleField;
    [SerializeField] private GameObject[] _AIPrefabs;
    [SerializeField] private Gamer _enemy;
    [SerializeField] private List<Unit> AIHandUnits;
    [SerializeField] private List<Unit> AIFieldUnits;
    [SerializeField] private Handler _enemyField;
    [SerializeField] private int _aiMoney;
    [SerializeField] private int _aiMoneyDelay;
    [SerializeField] private int _aiCountInField;
    [SerializeField] private int _aiWaveCount;
    [SerializeField] private int _aistaticGold;
    [SerializeField] private int _aiGoldIncome;
    [SerializeField] private int _strOfEnemy;
    public List<Unit> EnemyList;
    public GameObject CreatingSphere;
    // Start is called before the first frame update
   public void EnemySpawner(int lvl)
    {
        _strOfEnemy = lvl;
        //_battleField = Handler.Instance.BattleField;
        StartCoroutine(SpawnTimer());
        StartCoroutine(GoldIncome());
       
    }
    IEnumerator GoldIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(_aiMoneyDelay);
            int prevgold = _aiMoney;
            _aiMoney += MoneySystem.Instance.GiveMoney(AIFieldUnits);
            _aiMoney += _aistaticGold;
            _aiGoldIncome = _aiMoney - prevgold;
        }
    }

        IEnumerator SpawnTimer()
    {
        
        while (true)
        {
           
            yield return new WaitForSeconds(2f);
            AIFieldUnits = MatchControl.Instant.AIUnits;
           
           
            if (AIFieldUnits.Count < _aiCountInField)
            ChooseUnits();

            int enemycost =0;
             foreach (Unit enemy in MatchControl.Instant.Units)
            {
                enemycost += enemy.Price;
            }
            int aicost = 0;
            foreach (Unit aiunits in MatchControl.Instant.AIUnits)
            {
                aicost += aiunits.Price;
            }
            if (enemycost > aicost*5 && _aiMoney < _AIPrefabs[0].GetComponent<Unit>().Price)
            {
                //MatchControl.Instant.GameOver(1);
            }
        }
    }

    public Unit ChooseUnits()
    {
      
      List<Unit> tanks =  AIFieldUnits.FindAll(delegate (Unit Unit)
            {
                return Unit.TypeOfUnit == 1;
            });

        if(tanks.Count==0 && _aiMoney >= _AIPrefabs[1].GetComponent<Unit>().Price)
        {
            _aiMoney -= _AIPrefabs[1].GetComponent<Unit>().Price;
            Spawn(_AIPrefabs[1]);
        }


        if(_aiMoney > _AIPrefabs[2].GetComponent<Unit>().Price &&_aiGoldIncome >= _AIPrefabs[2].GetComponent<Unit>().Price)
        {
            _aiMoney -= _AIPrefabs[2].GetComponent<Unit>().Price;
            Spawn(_AIPrefabs[2]);
        }

        if (_aiMoney > _AIPrefabs[3].GetComponent<Unit>().Price && _aiGoldIncome >= _AIPrefabs[3].GetComponent<Unit>().Price)
        {
            _aiMoney -= _AIPrefabs[3].GetComponent<Unit>().Price;
            Spawn(_AIPrefabs[3]);
        }
        foreach (Unit aiUnit in AIFieldUnits)
        {
           
        }
        if ( _aiMoney>= _AIPrefabs[0].GetComponent<Unit>().Price)
        {
            _aiMoney -= _AIPrefabs[0].GetComponent<Unit>().Price;
            Spawn(_AIPrefabs[0]);
        }
      
        
        /* if (_enemyField.UnitsInField.Count > AIFieldUnits.Count)
         {
             foreach (Unit enemyUnit in _enemyField.UnitsInField)
             {
                 if (enemyUnit.TypeOfUnit == 0)
                 {
                     Spawn(_AIPrefabs[1]);
                 }
                 if (enemyUnit.TypeOfUnit == 1)
                 {
                     Spawn(_AIPrefabs[0]);
                 }
             }
         }
         else
         {
             Spawn(_AIPrefabs[2]);
         }*/
        return null;
    }

    public void Spawn(GameObject prefab)
    {
        Cell randomCell;
        Cell[,] grid = GridField.Instance.Grid;
        if(prefab.GetComponent<Unit>().TypeOfUnit == 0)
            randomCell = grid[Random.Range(0, grid.GetLength(0)), grid.GetLength(1)-1];
        else if(prefab.GetComponent<Unit>().TypeOfUnit == 1)
            randomCell = grid[Random.Range(0, grid.GetLength(0)), Random.Range(grid.GetLength(1) / 2, grid.GetLength(1)-2)];
        else
            randomCell = grid[Random.Range(0, grid.GetLength(0)), Random.Range(grid.GetLength(1) / 2, grid.GetLength(1))];
        if (randomCell.Walkable)
                {
           
            StartCoroutine(CreateTimer(prefab.GetComponent<Unit>().TimeToCreate, randomCell, prefab));
            return;
                }   
        
    }
    
    IEnumerator CreateTimer(float seconds,Cell randomCell, GameObject prefab)
    {
        GameObject creating = Instantiate(CreatingSphere, randomCell.WorldPosition + randomCell.Size / 2 + new Vector3(0, 0.2f, 0), Quaternion.identity);
       
        ParticleSystem ps = creating.GetComponent<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.duration = seconds;
        ps.Play();
        GameObject unit = Instantiate(prefab, randomCell.WorldPosition + randomCell.Size / 2, Quaternion.identity);

        randomCell.Walkable = false;
        unit.name = "enemy";
        unit.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        unit.GetComponent<FigurePresenter>().Mine(false);
        unit.GetComponent<Unit>().UnitSetup(_enemy, _battleField, null, 0, true, 1);

        unit.SetActive(false);

        EnemyList.Add(unit.GetComponent<Unit>());
        yield return new WaitForSeconds(seconds);
        Destroy(creating);
        unit.SetActive(true);
        unit.GetComponent<Unit>().Activate = true;
        unit.GetComponent<FigurePresenter>().Activated(true);
        MatchControl.Instant.ResetTargets(unit.GetComponent<Unit>());
        unit.GetComponent<Unit>().Find();
        
    }
}
