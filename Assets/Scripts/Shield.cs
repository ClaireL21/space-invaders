using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Laser" || collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if (collision.tag == "Alien")
        {
            Destroy(gameObject);
        }
    }
}
