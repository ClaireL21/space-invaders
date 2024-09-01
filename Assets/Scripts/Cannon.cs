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
    void Update()
    {
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
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObject.transform.position.x, minScreen.x + 1, maxScreen.x - 1), 
            0, gameObject.transform.position.z); 
    }
}
