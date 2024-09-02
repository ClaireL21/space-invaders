using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Vector3 thrust;
    //public int direction;


    // Start is called before the first frame update
    void Start()
    {
        // direction = 1;

        // travel straight in the x-axis
        thrust.x = 0.001f; //30.0f;

        // do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;

        // apply thrust once, no need to apply again
        //GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    public void ChangeDirection()
    {
        thrust.x *= -1;
        Debug.Log("Direction is " + thrust.x);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += thrust;
       
        /*Vector3 minScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));*/
        /*
       Vector3 pos = gameObject.transform.position;

       if (pos.x > maxScreen.x -1 || pos.x < minScreen.x +1)
       {
           direction *= -1;
           thrust *= direction;
       }
       // Vector3 pos = gameObject.transform.position;
       gameObject.transform.position = new Vector3(
           pos.x, 0, pos.z);
       */
        /*if (gameObject.transform.position.x > maxScreen.x / 2.0f)
        {
            thrust.x *= -1;
            Debug.Log("Update: alien gets called");
        }*/
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
