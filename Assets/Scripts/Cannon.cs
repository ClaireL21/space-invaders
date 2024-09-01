using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public GameObject laser;

    void Update()
    {
        // Update player movements
        Vector3 minScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.position += new Vector3(0.01f, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.position += new Vector3(-0.01f, 0, 0);
        }
       // Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObject.transform.position.x, minScreen.x + 1, maxScreen.x - 1), 
            0, gameObject.transform.position.z);

        // Check for player fire - left click
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 spawnPos = gameObject.transform.position;
            //spawnPos.z += 0.5f;
            // Instantiate the laser
            Instantiate(laser, spawnPos, Quaternion.identity);

           // GameObject obj = Instantiate(laser, spawnPos, Quaternion.identity) as GameObject;
        }
    }
}
