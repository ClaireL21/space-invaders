using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 thrust;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the Z-axis
        thrust.z = 250.0f;

        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;

        // set the direction it will travel in
       // GetComponent<Rigidbody>().MoveRotation(heading);

        // apply thrust once, no need to apply again
        GetComponent<Rigidbody>().AddRelativeForce(thrust);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
