using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public Vector3 distance;
    Cannon cannon;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("Cannon");
        cannon = g.GetComponent<Cannon>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.tag == "GroundBox")
        {
            Debug.Log("Ammunition hit ground");
            Die();
            cannon.bulletSupply = Mathf.Min(30, cannon.bulletSupply + 10);
        }
    }
    public GameObject deathExplosion;   // particle effect
    public AudioClip deathSound;
    public void Die()
    {
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
        AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);
        Destroy(gameObject);
    }
}
