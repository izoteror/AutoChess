using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUnit : MonoBehaviour
{
   public void Upgrade(Unit unit)
    {
        Debug.Log("Upgrade "+ unit.name);
    }
}
