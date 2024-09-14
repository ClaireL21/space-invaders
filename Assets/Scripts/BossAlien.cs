using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAlien : MonoBehaviour
{
    public Vector3 distance;
    public Camera orthoCam;

    public GameObject projectile;

    public float bossTimer;
    public float bossPeriod;

    int pointValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        distance.x = 0.008f;

        GameObject g = GameObject.Find("GlobalObject");
        orthoCam = g.GetComponent<Global>().orthoCam;

        bossTimer = 0f;
        bossPeriod = 2f;
        
    }

    // Update is called once per frame
    void Update()
    {
        float screenPadding = 100.0f;

        Vector3 maxHorizontal = orthoCam.ScreenToWorldPoint(new Vector3(Screen.width - screenPadding, 0, 0));
        Vector3 minHorizontal = orthoCam.ScreenToWorldPoint(new Vector3(screenPadding, 0, 0));

        if (gameObject.transform.position.x > maxHorizontal.x || gameObject.transform.position.x < minHorizontal.x)
        {
            ChangeDirection();
        }
        gameObject.transform.position += distance;

        bossTimer += Time.deltaTime;
        if (bossTimer > bossPeriod)
        {
            Shoot();
            bossTimer = 0;
        }

    }
    public void ChangeDirection()
    {
        distance.x *= -1;
    }

    public GameObject enemyBullet;

    public void Shoot()
    {
        Instantiate(enemyBullet, gameObject.transform.position,
            Quaternion.identity);
        // AudioSource.PlayClipAtPoint(bulletSound, gameObject.transform.position);
    }

    public void Die()
    {
        Destroy(gameObject);
        SpawnProjectiles();

        GameObject g = GameObject.Find("GlobalObject");
        Global globalObj = g.GetComponent<Global>();
        globalObj.score += pointValue;
    }

    // https://www.youtube.com/watch?v=NivKaNN7I00&t=179s
    void SpawnProjectiles()
    {
        float numProjectiles = 8;

        float angleStep = 360f / numProjectiles;
        float angle = 0f;

        Vector3 startPoint = gameObject.transform.position;
        float radius = 5f;
        float moveSpeed = 5f;

        for (int i = 0; i < numProjectiles; i++)
        {
            float projDirXPos = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projDirYPos = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projVector = new Vector3(projDirXPos, 0, projDirYPos);
            Vector3 projVectorMoveDir = (projVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody>().velocity = new Vector3(projVectorMoveDir.x, 0, projVectorMoveDir.z);

            angle += angleStep;
        }
    }
}
