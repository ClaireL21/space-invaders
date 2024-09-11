using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 distance;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the Z-axis
        distance.z = -0.2f; 
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            gameObject.transform.position += distance;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.tag == "Ground" && isActive)
        {
            isActive = false;
        }
    }
}
