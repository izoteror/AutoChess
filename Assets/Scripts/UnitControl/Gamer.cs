using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamer : MonoBehaviour
{
    private int health;
    [SerializeField] private int _team;

    public int Health { get => health; set
        { 
            health = value;

            Camera.main.GetComponent<UICounters>().HealthView(health,_team);

            if(health<=0)
            {
                health = 0;
                MatchControl.Instant.GameOver(Team);
            }
        } }

    public int Team { get => _team; set => _team = value; }

    private void Start()
    {
        Health = 100;
    }



    public void HealthDamage(int damage)
    {
        Health -= damage;
    }
}
