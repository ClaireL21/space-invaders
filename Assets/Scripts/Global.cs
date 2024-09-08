using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Global : MonoBehaviour
{
    // Spawning aliens
    public LinkedList<GameObject> aliensList;
    public LinkedList<LinkedList<GameObject>> alienGroups;
    public GameObject objToSpawn;
    public Vector3 originInScreenCoords;
    public int numAliensToSpawn;
    public int numRows;

    // Spawning alien bullets
    public float timer;
    public float shootPeriod;

    // Spawning mystery ship
    public float mysteryTimer;
    public float mysteryPeriod;
    public GameObject mysteryShip;

    // Score
    public int score;

    // Shield
    public GameObject shieldUnit;

    public Camera orthoCam; // This camera is used purely for calculations (OrthoCamCalculations)

    public GameOverScreen gameOverScreen;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        originInScreenCoords = orthoCam.WorldToScreenPoint(new Vector3(0, 0, 0));
        timer = 0;
        shootPeriod = 1.5f; // An alien shoots every 1.5 seconds
        mysteryTimer = 0;
        mysteryPeriod = 25.0f;

        // Initialize the alienGroups linked list for shooting
        alienGroups = new LinkedList<LinkedList<GameObject>>();

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
                    orthoCam.ScreenToWorldPoint(new Vector3(horizontalPos, verticalPos, originInScreenCoords.z)),
                    Quaternion.identity);
                Alien alien = alienObject.GetComponent<Alien>();
                if (rows == 0)
                {
                    alien.pointValue = 30;
                }
                else if (rows == 1 || rows == 2)
                {
                    alien.pointValue = 20;
                } else
                {
                    alien.pointValue = 10;
                }

                // add each alien spawned to a linked list of aliens - for shooting
                aliensList.AddLast(alienObject);
                if (alienGroups.Count > i)
                {
                    alienGroups.ElementAt(i).AddFirst(alienObject);
                } else
                {
                    LinkedList<GameObject> list = new LinkedList<GameObject>();
                    list.AddLast(alienObject);
                    alienGroups.AddLast(list);
                }
            }
        }

        // Spawn a row of shields
        int numShields = 4;
        float widthShieldUnit = 22.0f;
        float shieldHorPos = 400.0f;
        float shieldHorPadding = 225.0f;
        float oldHorPos;
        float shieldVertPos;

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
                       orthoCam.ScreenToWorldPoint(new Vector3(shieldHorPos, shieldVertPos, originInScreenCoords.z)),
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
        Vector3 maxHorizontal = orthoCam.ScreenToWorldPoint(new Vector3(Screen.width - screenPadding, 0, 0));
        Vector3 minHorizontal = orthoCam.ScreenToWorldPoint(new Vector3(screenPadding, 0, 0));
        
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

        int rand = Random.Range(0, alienGroups.Count);
        if (alienGroups.Count > 0)
        {
            GameObject shooterObj = alienGroups.ElementAt(rand).ElementAt(0);
            Alien shooterAlien = shooterObj.GetComponent<Alien>();
            if (timer > shootPeriod)
            {
                timer = 0;
                shooterAlien.Shoot();
            }
        }

        /* Control Mystery Ship Spawning */
        mysteryTimer += Time.deltaTime;
        if (mysteryTimer > mysteryPeriod)
        {
            Instantiate(mysteryShip,
                    orthoCam.ScreenToWorldPoint(new Vector3(0, Screen.height - 100.0f, originInScreenCoords.z)),
                    Quaternion.identity);
            mysteryTimer = 0;
        }
    }

    public void GameOver()
    {
        gameOverScreen.Setup(score);
    }

    public void DestroyAllAliens()
    {
        for (int i = 0; i < aliensList.Count; i++)
        {
            Alien alien = aliensList.ElementAt(i).GetComponent<Alien>();
            Destroy(alien);
        }
        alienGroups.Clear();
        aliensList.Clear();
    }
}
