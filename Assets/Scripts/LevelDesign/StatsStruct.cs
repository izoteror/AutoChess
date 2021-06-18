using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatsStruct
{
    public int Team;
    public float Health;
    public float Atack;
    public float Speed;
    public float AtackSpeed;
    public float MoneyMining;
    public float Price;
    public int Lvl;



    public StatsStruct(int team, float health, float atack, float speed, float atackSpeed, int lvl, float moneymine, float price)
    {
        Team = team;
        Lvl = lvl;
        Health = health;
        Atack = atack;
        Speed = speed;
        AtackSpeed = atackSpeed;
        MoneyMining = moneymine;
        Price = price;
        

    }

   
}
