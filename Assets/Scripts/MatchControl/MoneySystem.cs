using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private List<Unit> unitsInHand;
    private List<Unit> aiunits;
    [SerializeField] private float moneyTimer;
   [SerializeField] private int money;
    public int AIMoney;
    public static MoneySystem Instance;

    public int Money { get => money; 
        set
        {
            if (value >= 0)
            {
                money = value;  
            }
            Camera.main.GetComponent<UICounters>().MoneyView(money);

        }
    }

    void Start()
    {
        if (Instance == null)
            Instance = this;

        Camera.main.GetComponent<UICounters>().MoneyView(money);
        unitsInHand = GetComponent<Handler>().UnitsInField;

        StartCoroutine(SetMoney());
    }
    
    IEnumerator SetMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(moneyTimer);
            Money += GiveMoney(unitsInHand);
            Money += Handler.Instance.MoneyIncome;
        }
    }

    public int GiveMoney(List<Unit> _Units)
    {
        int money = 0;
        foreach(Unit Unit in _Units)
        {
            money += Unit.MoneyMining;
        }

        return money;
    }
}
