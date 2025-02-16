using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //Destroy(collision.gameObject);
            FindAnyObjectByType<BulletPool>().ReturnBullet(collision.gameObject);
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            //Destroy(collision.gameObject);
            FindAnyObjectByType<BulletPool>().ReturnEnemyBullet(collision.gameObject);
        }
    }
}
