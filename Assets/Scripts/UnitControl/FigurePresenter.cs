using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurePresenter : MonoBehaviour
{
    private Unit _Unit;
    private float _animspeedAtack = 0.2f;
    private Color _startcolor  = new Color();

    public Animator AnimatorUnit;

    private void Awake()
    {
        AnimatorUnit = GetComponent<Animator>();
    }

    void Start()
    {
        AnimatorUnit = GetComponent<Animator>();

    }
    
    public void Mine(bool mining)
    {
        AnimatorUnit.SetBool("Mining", mining);
    }

    public void Atack(bool value, float speed)
    { 
        AnimatorUnit.SetBool("Atack", value);
        AnimatorUnit.SetFloat("AtackSpeed", 1/speed);
       
    }

    public void Activated(bool state)
    {
        AnimatorUnit.SetBool("Activate", state);
    }

    public void Happy(int count)
    {
        AnimatorUnit.SetInteger("EnemyCount", count);
    }

    public void Damage()
    {
        AnimatorUnit.SetBool("Atacked", true);
   
    }

    public void Move(float speed)
    {
        AnimatorUnit.SetFloat("Speed", speed);
        
    }

    public void Stop()
    {

        AnimatorUnit.SetFloat("Speed", 0);
    }

}
