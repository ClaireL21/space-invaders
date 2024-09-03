using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Global : MonoBehaviour
{
    // Variables for spawning aliens
    public LinkedList<GameObject> aliensList;
    public LinkedList<LinkedList<GameObject>> alienGroups;
    public GameObject objToSpawn;
    public Vector3 originInScreenCoords;
    public int numAliensToSpawn;
    public int numRows;

    // Variables for spawning alien bullets
    public float timer;
    public float shootPeriod;

    // Score
    public int score;

    // Shield
    public GameObject shieldUnit;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        timer = 0;
        shootPeriod = 1.5f; // An alien shoots every 1.5 seconds

        // Initialize the alienGroups linked list
        alienGroups = new LinkedList<LinkedList<GameObject>>();
        /*for (int i = 0; i < numAliensToSpawn; i ++)
        {
            LinkedList<GameObject> list = new LinkedList<GameObject>();
            alienGroups.AddLast(list);
            Debug.Log("alien groups size inside: " + alienGroups.Count);

        }
        Debug.Log("alien groups size : " + alienGroups.Count);*/

        // Spawn an array of aliens
        float width = Screen.width;
        float height = Screen.height;
        float padding = 400.0f;
        numAliensToSpawn = 11;
        numRows = 5;

        float vertPadding = 200.0f; // from top of scene 

        aliensList = new LinkedList<GameObject>();

        for (int rows = 0; rows < numRows; rows++)
        {
            for (int i = 0; i < numAliensToSpawn; i++)
            {
                float horizontalPos = i * ((width - padding * 2) / (numAliensToSpawn - 1)) + padding;

                // height / 3.0f - want aliens to take up a third of the screen
                float verticalPos = height - vertPadding - rows * (( height / 3.0f) / (numRows - 1));
                
                GameObject alienObject = Instantiate(objToSpawn,
                    Camera.main.ScreenToWorldPoint(new Vector3(horizontalPos, verticalPos, originInScreenCoords.z)),
                    Quaternion.identity);

                // add each alien spawned to a linked list of aliens
                aliensList.AddLast(alienObject);
                //Debug.Log("hello");
                if (alienGroups.Count > i)
                {
                    alienGroups.ElementAt(i).AddFirst(alienObject);
                } else
                {
                    LinkedList<GameObject> list = new LinkedList<GameObject>();
                    list.AddLast(alienObject);
                    alienGroups.AddLast(list);
                }
                //alienGroups.ElementAt(i).AddFirst(alienObject); // need to instantiate empty linked list and add it
                //Debug.Log("bye");
            }
        }

        Debug.Log("aliengroups: " + alienGroups);
        // Spawn a row of shields
        int numShields = 4;
        float widthShieldUnit = 22.0f; // TODO: find this using math // Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector3(0.2f, 0, 0)).x);
        float shieldHorPos = 400.0f;
        float shieldHorPadding = 225.0f;
        float oldHorPos;
        float shieldVertPos;
        Debug.Log("Width shield unit: " + widthShieldUnit);
        for (int i = 0; i < numShields; i++)
        {
            shieldVertPos = 200.0f;
            oldHorPos = shieldHorPos;

            for (int r = 0; r < 3; r++)
            {
                shieldHorPos = oldHorPos;
                for (int unit = 0; unit < 5; unit++)
                {
                    Instantiate(shieldUnit,
                       Camera.main.ScreenToWorldPoint(new Vector3(shieldHorPos, shieldVertPos, originInScreenCoords.z)),
                       Quaternion.identity);
                    shieldHorPos += widthShieldUnit;
                }
                shieldVertPos += widthShieldUnit;
            }
            shieldHorPos += shieldHorPadding;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Control Alien movement */
        float screenPadding = 100.0f;
        bool change = false;

        /* Check if any alien object goes past bounds
        If so, then set change direction to be true */
        Vector3 maxHorizontal = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - screenPadding, 0, 0));
        Vector3 minHorizontal = Camera.main.ScreenToWorldPoint(new Vector3(screenPadding, 0, 0));
        
        for (int i = 0; i < aliensList.Count; i++)
        {
            Alien alien = aliensList.ElementAt(i).GetComponent<Alien>();
            if (alien.transform.position.x > maxHorizontal.x || alien.transform.position.x < minHorizontal.x)
            {
                change = true;
            }
        }

        // If we need to change the direction, change it here
        if (change)
        {
            for (int i = 0; i < aliensList.Count; i++)
            {
                Alien alien = aliensList.ElementAt(i).GetComponent<Alien>();
                alien.ChangeDirection();
            }
        }

        /* Control Alien Shooting */
        timer += Time.deltaTime;
        Alien shooterAlien; // = aliensList.ElementAt(0).GetComponent<Alien>();
        LinkedList<GameObject> shooters = new LinkedList<GameObject>();

        /* for (int i = 0; i < alienGroups.Count; i++)
         {
             //sGameObject curr = 
             shooters.AddLast(alienGroups.ElementAt(i).First);
         }*/

        int rand = Random.Range(0, alienGroups.Count);
        GameObject shooterObj = alienGroups.ElementAt(rand).ElementAt(0);
        shooterAlien = shooterObj.GetComponent<Alien>(); // GetComponent<Alien>();
        if (timer > shootPeriod)
        {
            timer = 0;
            shooterAlien.Shoot();
        }
    }
}
