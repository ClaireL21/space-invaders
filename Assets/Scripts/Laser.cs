using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the Z-axis
        distance.z = 0.2f;
    }

    public AudioClip laserSound;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += distance;
       // AudioSource.PlayClipAtPoint(laserSound, gameObject.transform.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Alien"))
        {
            Alien alien = collider.GetComponent<Alien>();
            alien.Die();
            Destroy(gameObject);
        } else if (collider.CompareTag("MysteryShip")) {
            Mystery mystery = collider.GetComponent<Mystery>();
            mystery.Die();
            Destroy(gameObject);
        }
    }
}
