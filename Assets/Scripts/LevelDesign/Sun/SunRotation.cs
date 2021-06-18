using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        rotate += new Vector3(3*Time.deltaTime, 0, 0);
        transform.rotation = Quaternion.Euler(rotate);
    }
}
