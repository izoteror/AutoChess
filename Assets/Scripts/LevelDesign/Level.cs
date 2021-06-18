using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int LvlNumber;
    private bool active = false;
    [SerializeField] private Text _number;

    // Start is called before the first frame update
    void Start()
    {
        _number.text = LvlNumber.ToString();
        
    }

    public void Activate()
    {
        GetComponent<Image>().color = new Color(255,255,0);
        active = true;
    }

    public void LevelDone()
    {
        GetComponent<Image>().color = new Color(57, 159,69);
        active = true;
    }

    public void ClickButton()
    {
        if (active)
        {
            SceneDataTranslator.LvlNumber = LvlNumber;
            SceneDataTranslator.SlimeT1 = UnitsStats.slime;
            SceneDataTranslator.TurtleT1 = UnitsStats.turtle;
            SceneDataTranslator.KnightT1 = UnitsStats.knight;
            SceneDataTranslator.SlimeT2 = LvlEnemyTactics.MultiplyKoef(LvlEnemyTactics.slime, SceneDataTranslator.EnemyLvlKoef * LvlNumber);
            SceneDataTranslator.TurtleT2 = LvlEnemyTactics.MultiplyKoef(LvlEnemyTactics.turtle, SceneDataTranslator.EnemyLvlKoef * LvlNumber);
            SceneManager.LoadScene("WarScene");
        }
    }
}
