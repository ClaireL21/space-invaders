using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 distance;
    bool isActive;

    public Vector3 thrust;
    //public Quaternion heading;


    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the Z-axis
        distance.z = 0.02f;
        //gameObject.GetComponent<Rigidbody>().AddForce(distance * 1000);

        thrust.z = 700.0f;
        GetComponent<Rigidbody>().drag = 0;
      //  GetComponent<Rigidbody>().MoveRotation(heading);
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
        isActive = true;
    }

    public AudioClip laserSound;
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //gameObject.transform.position += distance;
        }
        // AudioSource.PlayClipAtPoint(laserSound, gameObject.transform.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
      if (isActive)
        {
        Collider collider = collision.collider;
        if (collider.CompareTag("Alien"))
        {
            Alien alien = collider.GetComponent<Alien>();
            if (alien.isActive)
            {
                alien.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                // alien.GetComponent<Rigidbody>().AddForce(distance * 10);
                alien.Die(false);
            }
            
            //Destroy(gameObject);
        }
        else if (collider.CompareTag("MysteryShip"))
        {
            Mystery mystery = collider.GetComponent<Mystery>();
            mystery.Die();
            //Destroy(gameObject);
        } else if (collider.CompareTag("BossAlien"))
        {
            BossAlien boss = collider.GetComponent<BossAlien>();
            boss.Die();
            Destroy(gameObject);
        } 
        /* else if (collider.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }*/
        isActive = false;
       }
        
    }
}
