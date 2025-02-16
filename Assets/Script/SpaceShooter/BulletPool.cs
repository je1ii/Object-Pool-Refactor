using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;

    private ObjectPool<GameObject> playerBulletPool;
    private ObjectPool<GameObject> enemyBulletPool;

    void Start()
    {
        playerBulletPool = new ObjectPool<GameObject>
            (
                CreatePlayerBullet, 
                OnTakeBullet, 
                OnReturnBullet, 
                OnDestroyBullet, 
                false,
                10
            );

        enemyBulletPool = new ObjectPool<GameObject>
            (
                CreateEnemyBullet,
                OnTakeBullet,
                OnReturnBullet,
                OnDestroyBullet,
                false,
                15
            );
    }

    GameObject CreatePlayerBullet()
    {
        return Instantiate(playerBulletPrefab);
    }

    GameObject CreateEnemyBullet()
    {
        return Instantiate(enemyBulletPrefab);
    }

    void OnTakeBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    void OnReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    void OnDestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    public void FireBullet(Vector3 pos, Quaternion rot, float speed) 
    {
        GameObject bullet = playerBulletPool.Get();
        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = new Vector2(0f, speed);
    }

    public void FireEnemyBullet(Vector3 pos, Quaternion rot, float speed)
    {
        GameObject bullet = enemyBulletPool.Get();
        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = new Vector2(0f, -speed);
    }

    public void ReturnBullet(GameObject bullet)
    {
        playerBulletPool.Release(bullet);
    }
    public void ReturnEnemyBullet(GameObject bullet)
    {
        enemyBulletPool.Release(bullet);
    }

}
