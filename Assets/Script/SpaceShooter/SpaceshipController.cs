using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SpaceshipController : MonoBehaviour
{
    public List<EnemySpaceShooter> Enemies;

    public float Speed;
    public float BulletSpeed;
    public Transform BulletSpawnHere;
    public GameObject GameClearScreen;
    public TextMeshProUGUI textValue,hpValue;
    public int score;
    public int hitponts;
    bool isGameClear = false;
    private int storeHP;
    public GameObject GameOverScreen;
    private bool canMove = true;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        storeHP = hitponts;
    }

    // Update is called once per frame
    void Update()
    {
        textValue.text = score.ToString();
        hpValue.text = hitponts.ToString();
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            //SpawnBullet(); 
            FindAnyObjectByType<BulletPool>().FireBullet(BulletSpawnHere.position, Quaternion.identity, BulletSpeed);
        }

        /*if (hitponts <= 0)
        {
            canShoot = false;
            canMove = false;
            GameOverScreen.SetActive(true);
            hitponts = 0;
        }

        OnGameClear();

        if (isGameClear && hitponts > 0)
        {

            isGameClear = false;

        }*/
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 moveInput = new Vector3(horizontalInput, 0, 0);
            transform.position += Time.deltaTime * Speed * moveInput;
        }
    }

    /*public void SpawnBullet()
    {
        //Instantiate to clone a game object
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnHere.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = new Vector2(0f, BulletSpeed);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            hitponts--;
            //Destroy(collision.gameObject);
            FindAnyObjectByType<BulletPool>().ReturnEnemyBullet(collision.gameObject);
        }
    }

    public void RestartGame()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].transform.position = Enemies[i].InitialPosition;
            Enemies[i].gameObject.SetActive(false);
            //Delays the call of a method in Ienumerator
            StartCoroutine(DelayEnemiesActive());
        }
        canMove = true;
        canShoot = true;
        hitponts = storeHP;
        score = 0;
        isGameClear = false;
        GameOverScreen.SetActive(false);
    }
    IEnumerator DelayEnemiesActive()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].gameObject.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnGameClear()
    {
        isGameClear = true;
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject.activeSelf)
            {
                isGameClear = false;
                break;
            }
        }
        if (isGameClear)
        {
            GameClearScreen.SetActive(true);
        }
    }
}

