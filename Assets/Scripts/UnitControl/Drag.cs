using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public DragSystem Drager;

   

    private void OnMouseDrag()
    {
        Drager.Draging(GetComponent<Unit>());
    }

    private void OnMouseUp()
    {
        Drager.LocateUnit(GetComponent<Unit>());
    }
}
