using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystery : MonoBehaviour
{
    public Vector3 distance;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        // travel straight in the x-axis
        distance.x = 0.009f;
        pointValue = Random.Range(10, 30) * 10;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += distance;
    }

    public GameObject deathExplosion;
    public void Die()
    {
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;
        Destroy(gameObject);
    }
}
