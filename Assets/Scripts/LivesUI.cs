using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    Cannon cannonObj;
    TMP_Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("Cannon");
        cannonObj = g.GetComponent<Cannon>();
        livesText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = cannonObj.lives.ToString();
    }
}
