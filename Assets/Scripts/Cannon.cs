using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Camera orthoCam;  // This camera is used purely for calculations (OrthoCamCalculations)
    public Camera shakeOrthoCam;  // Orth cam for shaking (used for user's view rather than calculations)
    public Camera shakePerspCam;  // Persp cam for shaking (used for user's view, in conjunction with ortho)
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    public GameObject laser;

    void Update()
    {
        /* Update player movements */
        Vector3 minScreen = orthoCam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreen = orthoCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.position += new Vector3(0.01f, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.position += new Vector3(-0.01f, 0, 0);
        }
        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObject.transform.position.x, minScreen.x + 1, maxScreen.x - 1), 
            0, gameObject.transform.position.z);

        /* Check for player fire - left click */
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.z += 0.5f;

            // Instantiate the laser
            Instantiate(laser, spawnPos, Quaternion.identity);
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "EnemyBullet")
        {
            Bullet bullet = collider.GetComponent<Bullet>();

            //Destroy(collision.gameObject);
            if (bullet.isActive)
            {
                Die();
                Debug.Log("Cannon died from bullet");
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
                Debug.Log("Cannon died from alien");

                alien.isActive = false;
            }
        }
    }
}
