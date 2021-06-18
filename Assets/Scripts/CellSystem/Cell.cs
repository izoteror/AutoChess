using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private bool walkable;
    public Vector3 WorldPosition;
    public Vector3 Size;
    public int X;
    public int Y;
    public Cell Parent;
    public bool Open;
    public int Gcost;
    public int Hcost;
    public Unit UnitOnMe;

    public int Fcost
    {
        get
        {
            return Gcost + Hcost;
        }
    }

    public bool Walkable { get => walkable; set => walkable = value; }

    public Cell(bool _walkable, Vector3 _worldposition, Vector3 size,int x, int y )
    {
        X = x;
        Y = y;
        Walkable = _walkable;
        WorldPosition = _worldposition;
        Size = size;

    }  
}
