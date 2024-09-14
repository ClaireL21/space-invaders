using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Vector3 distance;
    public int pointValue;
    bool moveDown;
    public bool isActive;

    public float alienTimer;
    public float alienPeriod;
    public int alienIters;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the x-axis
       // GameObject g = GameObject.Find("GlobalObject");
        distance.x = 0.002f; // g.GetComponent<Global>().alienSpeed;
       // distance.x = 0.002f; // 0.002f
        moveDown = false;
        isActive = true;

        alienTimer = 0;
        alienPeriod = 3;
        alienIters = 1;
    }

    public void ChangeDirection()
    {
        if (isActive)
        {
            distance.x *= -1;
            moveDown = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (moveDown)
            {
                gameObject.transform.position += new Vector3(0, 0, -0.3f);
                moveDown = false;
            } else
            {
               /* GameObject g = GameObject.Find("GlobalObject");
                distance.x = g.GetComponent<Global>().alienSpeed;*/
            }
            alienTimer += Time.deltaTime;
            if (alienTimer > alienIters * alienPeriod)
            {
                alienIters += 1;
                distance.x += (Mathf.Abs(distance.x) / distance.x) * 0.0005f; // 1.2f;
            }
            gameObject.transform.position += distance;
        }
    }

    public GameObject enemyBullet;
    public AudioClip bulletSound;
    public void Shoot()
    {
        Instantiate(enemyBullet, gameObject.transform.position, 
            Quaternion.identity);
        // AudioSource.PlayClipAtPoint(bulletSound, gameObject.transform.position);
    }

    public GameObject deathExplosion;   // particle effect
    public AudioClip deathSound;
    public void Die(bool destroy)
    {
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
        AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);

        // Delete that alien from the linked list of aliens
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.aliensList.Remove(gameObject);

        // And delete it from the Groups representation of aliens (for shooting)
        // remove the group if all aliens in the group are dead
        for (int i = 0; i < g.alienGroups.Count; i++)
        {
            LinkedList<GameObject> currList = g.alienGroups.ElementAt(i);

            if (currList.Contains(gameObject))
            {
                currList.Remove(gameObject);
                if (currList.Count == 0)
                {
                    g.alienGroups.Remove(currList);
                }
                break;
            }
        }
        g.score += pointValue;
       // gameObject.GetComponent<Rigidbody>().AddForce(distance);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f, 50.0f, 0.0f));

        isActive = false;
        if (destroy)
        {
            Destroy(gameObject);
        }
       // Destroy(gameObject);
    }
}
