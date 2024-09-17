using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Camera orthoCam;  // This camera is used purely for calculations (OrthoCamCalculations)
    public Camera shakeOrthoCam;  // Orth cam for shaking (used for user's view rather than calculations)
    public Camera shakePerspCam;  // Persp cam for shaking (used for user's view, in conjunction with ortho)
    public float speed;
    public int lives;
    public Ground ground;

   // public int numAliens;
    
    // Bullet supply
    public int bulletSupply;

    // Start is called before the first frame update
    void Start()
    {
        lives = 5;
        bulletSupply = 20;
        speed = 0.02f;
        //numAliens = 0;

        /*GameObject g = GameObject.Find("Ground");
        ground = g.GetComponent<Ground>();*/
    }

    // Update is called once per frame
    public GameObject laser;

    void setSpeed()
    {
        if (ground.numAliens >  0)
        {
            float direction = Mathf.Abs(speed) / speed;

            speed = Mathf.Max(0.004f, 0.02f - 0.002f * ground.numAliens);
            speed *= direction;


            /*speed = 0.01f;
            speed += -1 * (Mathf.Abs(speed) / speed) * ground.numAliens * 0.5f;*/
        }
    }

    void Update()
    {
        /* Update player movements */
        Vector3 minScreen = orthoCam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreen = orthoCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));

        setSpeed();

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            speed = Mathf.Abs(speed);
            gameObject.transform.position += new Vector3(speed, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            speed = -1 * Mathf.Abs(speed);
            gameObject.transform.position += new Vector3(speed, 0, 0);
        }
       /* else if (Input.GetAxisRaw("Vertical") > 0)
        {
            gameObject.transform.position += new Vector3(+0.01f, 0, 0);
        }*/
        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObject.transform.position.x, minScreen.x + 3, maxScreen.x - 3), 
            0, gameObject.transform.position.z);

        /* Check for player fire - left click */
        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletSupply  > 0)
            {
                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.z += 0.5f;

                // Instantiate the laser
                Instantiate(laser, spawnPos, Quaternion.identity);
                bulletSupply -= 1;
            }
            
        }
    }

    public GameObject deathExplosion;   // particle effect
    public AudioClip deathSound;
    public AudioClip gameOverSound;
    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);

        GameObject obj = GameObject.Find("ButtonControl");
        Button b = obj.GetComponent<Button>();
        
        if (b.orthoCamera.enabled)
        {
            shakeOrthoCam.GetComponent<CamShake>().start = true;
        } else
        {
            shakePerspCam.GetComponent<CamShake>().start = true;
        }

        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));

        lives -= 1;
        if (lives == 0)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, gameObject.transform.position);
            Destroy(gameObject);
            GameObject g = GameObject.Find("GlobalObject");
            Global globalObj = g.GetComponent<Global>();
            globalObj.GameOver();
            globalObj.CheckHighScore();
            globalObj.SetHighScoreUI();

            globalObj.DestroyAllAliens();
            /*globalObj.alienGroups.Clear();
            globalObj.aliensList.Clear();*/
            //scoreText = gameObject.GetComponent<TMP_Text>();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.tag == "EnemyBullet")
        {
            Bullet bullet = collider.GetComponent<Bullet>();

            //Destroy(collision.gameObject);
            if (bullet.isActive)
            {
                Die();
                bullet.isActive = false;
            }
            collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if (collider.tag == "Alien")
        {
            Alien alien = collider.GetComponent<Alien>();
            if (alien.isActive)
            {
                Die();

                alien.isActive = false;
            }
        }
    }
}
