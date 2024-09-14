using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public int numAliens;
    // Start is called before the first frame update
    void Start()
    {
        numAliens = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        
        if (collider.tag == "Alien")
        {
            Debug.Log("Hit aline");
            numAliens++;
            /*Alien alien = collider.GetComponent<Alien>();
            if (alien.isActive)
            {
                Die();
                Debug.Log("Cannon died from alien");

                alien.isActive = false;
            }*/
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.tag == "Alien")
        {
            Debug.Log("Left aline");
            numAliens--;
            /*Alien alien = collider.GetComponent<Alien>();
            if (alien.isActive)
            {
                Die();
                Debug.Log("Cannon died from alien");

                alien.isActive = false;
            }*/
        }
    }
}
