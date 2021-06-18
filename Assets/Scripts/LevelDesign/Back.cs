using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public int Number;
    public List<Level> LevelListBack;

    public List<Level> GetListLevel()
    {
        return LevelListBack;
    }
    // Start is called before the first frame update
    public void GenerateBack()
    {
        var Child = GetComponentsInChildren<Transform>();
        int i = 1;
        foreach(Transform child in Child)
        {
            if(child.tag == "Lvl")
            {
                child.GetComponent<Level>().LvlNumber = Number*5 + i;
                LevelListBack.Add(child.GetComponent<Level>());
                i++;
            }
           
        }
    }

    
}
