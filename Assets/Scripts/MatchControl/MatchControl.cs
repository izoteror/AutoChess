using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchControl : MonoBehaviour
{
    [SerializeField] private List<Unit> playerUnits;
    [SerializeField] private List<Unit> aiUnits;

    public List<Unit> Units { get => playerUnits; }
    public List<Unit> AIUnits { get => aiUnits; set => aiUnits = value;}
    public static MatchControl Instant;
   
    void Start()
    {
        Instant = this;

        foreach (Unit Unit in playerUnits)
        {
            ResetTargets(Unit);
        }

    }

    public void RestartLvl()
    {
        Time.timeScale = 1;
        SceneDataTranslator.LvlNumber--;
        SaveLoad.Save();
        SceneManager.LoadScene("WorldMap");
        
        
    }
    public void NextLvl()
    {
        Time.timeScale = 1;
        // SceneDataTranslator.LvlNumber--;
        SaveLoad.Save();
        SceneManager.LoadScene("WorldMap");
        

    }

    public void GameOver(int _team)
    {
        Time.timeScale = 0;
        Camera.main.GetComponent<UICounters>().GameOVerUI(_team);
    }

    public void ResetTargets(Unit _unit)
    {
        if (_unit.Team == 0)
            playerUnits.Add(_unit);
        else if (_unit.Team == 1)
            AIUnits.Add(_unit);
            _unit.OnDeath += DeleteFromScene;
            //  Unit.Find();


    }

    public void DeleteFromScene(Unit _deadman)
    {
        
            _deadman.OnDeath -= DeleteFromScene;
        if (_deadman.Team == 0)
            playerUnits.Remove(_deadman);
        else if (_deadman.Team == 1)
            AIUnits.Remove(_deadman);
        Destroy(_deadman.gameObject);
    }
}
