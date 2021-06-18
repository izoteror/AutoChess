using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartControl : MonoBehaviour
{

    public EnemySpawn EnemySpawner;
    private UICounters UIControl;
    // Start is called before the first frame update

    private void Start()
    {
        UIControl = GetComponent<UICounters>();
        StartCoroutine("StartGreating");

    }

    IEnumerator StartGreating()
    {

        UIControl.ShowGreating("The enemy is ahead! Lvl: " + SceneDataTranslator.LvlNumber);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= 4; i++)
        {
            yield return new WaitForSeconds(1f);
            
             if(i == 3)
                GetComponent<UICounters>().ShowGreating("GO!");
             else
                GetComponent<UICounters>().ShowGreating((3 - i).ToString());
        }
       
        UIControl.HideGreating();
        EnemySpawner.EnemySpawner(SceneDataTranslator.LvlNumber);

    }
}
