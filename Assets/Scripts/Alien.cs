using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the x-axis
        distance.x = 0.001f;

        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;
    }

    public void ChangeDirection()
    {
        distance.x *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += distance;
    }

    public GameObject deathExplosion;
    public void Die()
    {
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));

        // Delete that alien from the linked list of aliens
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.aliensList.Remove(gameObject);
        Destroy(gameObject);
    }
}
