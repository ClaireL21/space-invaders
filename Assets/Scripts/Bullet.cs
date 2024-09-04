using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the Z-axis
        distance.z = -0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += distance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
       /* if (collider.CompareTag("Cannon"))
        {
            Cannon cannon = collider.GetComponent<Cannon>();
            cannon.Die();
            Destroy(gameObject);
        }*/

        /*else if (collider.CompareTag("Shield"))
        {
            Debug.Log("Shield collided");
            Shield shield = collider.GetComponent<Shield>();
            shield.Die();
            Destroy(gameObject);
        }*/
    }
}
