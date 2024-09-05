using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Vector3 distance;
    public int pointValue;
    bool moveDown;

    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the x-axis
        distance.x = 0.002f;
        moveDown = false;
    }

    public void ChangeDirection()
    {
        distance.x *= -1;
        moveDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDown)
        {
            gameObject.transform.position += new Vector3(0, 0, -0.3f);
            moveDown = false;
        }
        gameObject.transform.position += distance;
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
    public void Die()
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
        Destroy(gameObject);
    }
}
