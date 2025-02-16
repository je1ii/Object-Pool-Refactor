using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShooter : MonoBehaviour
{
    public SpaceshipController SpaceShip;
    public int health;
    public float minFR, MaxFR;
    private float FireRate;
    private float storedFireRate;
    public float BulletSpeed;

    public float moveSpeed;
    public float moveInterval;

    public Vector3 InitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
        //minFR = 1
        //maxFR = 5
        FireRate = Random.Range(minFR, MaxFR);
        storedFireRate = FireRate;
        //InvokeRepeating
        InvokeRepeating("MoveEnemy",5, moveInterval);
        
    }


    // Update is called once per frame
    void Update()
    {
        FireRate -= Time.deltaTime;
        if (FireRate <= 0)
        {
            //SpawnBullet();
            FindAnyObjectByType<BulletPool>().FireEnemyBullet(transform.position, Quaternion.identity, BulletSpeed);
            FireRate = storedFireRate;
        }
        if (health <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                SpaceShip.score++;
                gameObject.SetActive(false);
            }
            //Destroy(collision.gameObject);
            FindAnyObjectByType<BulletPool>().ReturnBullet(collision.gameObject);
        }
    }

    /*public void SpawnBullet()
    {
        //Instantiate to clone a game object
        GameObject bullet = Instantiate(BulletPrefab,transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = new Vector2(0f, -BulletSpeed);
    }*/
    
    public void MoveEnemy()
    {
        //Moves the enemy downwards aloth the y axis.
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
