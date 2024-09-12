using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    Cannon cannon;
    TMP_Text bulletText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("Cannon");
        cannon = g.GetComponent<Cannon>();
        bulletText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = cannon.bulletSupply.ToString();
    }
}
