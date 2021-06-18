using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICounters : MonoBehaviour
{
    public Text MoneyCounter;
    public Text HealthGamer;
    public Text HealthEnemy;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject StartPanel;


    private void Awake()
    {

        GameOverPanel.SetActive(false);
        WinPanel.SetActive(false);
        StartPanel.SetActive(true);
       
       
    }
    public void ShowGreating(string text)
    {
        StartPanel.GetComponentInChildren<Text>().text = text;
    }
    
    public void HideGreating()
    {
        StartPanel.SetActive(false);
    }
   

    public void MoneyView(int money)
    {
        MoneyCounter.text =  money.ToString() +" $";
    }


    public void GameOVerUI(int team)
    {
        if(team ==0)
        GameOverPanel.SetActive(true);
        if (team == 1)
            WinPanel.SetActive(true);

    }
    public void HealthView(int health, int team)
    {
        if(team == 0)
        {
            HealthGamer.text = "HP: "+health.ToString();
        }
        else if(team == 1)
        {
            HealthEnemy.text = "HP: "+ health.ToString();
        }
    }
}
