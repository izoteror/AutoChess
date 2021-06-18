using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnit : MonoBehaviour
{
    public Slider HealthBar;
    public GameObject UpgradingImage;
    [SerializeField] private Image _fill;
    private Unit Unit;
    private void Start()
    {
        
        Unit = GetComponent<Unit>();
        UpgradingImage.SetActive(false);
       

        if (Unit.Team == 0)
        {
            _fill.color = Color.green;
        }
        else if (Unit.Team == 1)
        {
            _fill.color = Color.red;
        }
       
        HealthBar.maxValue = Unit.Health;
        HealthBar.value = Unit.Health;
    }

    
    public void ShineOnUpgrade()
    {
        UpgradingImage.SetActive(true);
    }
    public void ShineOffUpgrade()
    {
        UpgradingImage.SetActive(false);
    }

    public void HealthBarChanger()
    {
        HealthBar.value = Unit.Health;
    }
}
