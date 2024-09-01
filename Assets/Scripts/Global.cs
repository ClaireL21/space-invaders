using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public GameObject objToSpawn;
    public Vector3 originInScreenCoords;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));

        // Spawning aliens
        float width = Screen.width;
        float height = Screen.height;
        float padding = 300.0f;
        int numAliensToSpawn = 11;
        int numRows = 5;
       // float vertSpawn = 200.0f;
        float vertPadding = 200.0f; // from top of scene 

        for (int rows = numRows - 1; rows >= 0; rows--)
        {
            for (int i = 0; i < numAliensToSpawn; i++)
            {
                float horizontalPos = i * ((width - padding * 2) / (numAliensToSpawn - 1)) + padding; //Random.Range(0.0f, width);
                                                                                                      //Debug.Log("width is "+ width);
                // height / 2.0f - want aliens to take up half the screen
                float verticalPos = height - vertPadding - rows * (( height / 3.0f) / (numRows - 1));
                Instantiate(objToSpawn,
                    Camera.main.ScreenToWorldPoint(new Vector3(horizontalPos, verticalPos, originInScreenCoords.z)),
                    Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
