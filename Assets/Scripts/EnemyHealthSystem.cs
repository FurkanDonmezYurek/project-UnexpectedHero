using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    GameObject Enemy;
    Rigidbody2D enemyRb;
    public float health;
    GameObject Player;
    float pushRotation;
    public float pushForce;
    public float deadTime;
    bool deadStarted;

    //Game Over Screen
    public GameObject gameOverScreen;
    public GameObject inGameScreen;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemy = this.gameObject;
        enemyRb = Enemy.GetComponent<Rigidbody2D>();
        deadStarted = false;
    }

    void Update()
    {
        //Hangi tarafa itilecek
        pushRotation = Player.transform.position.x - Enemy.transform.position.x;
        //Enemy Destroy

        if (health <= 0)
        {
            deadStarted = true;
            if (deadStarted)
            {
                StartCoroutine(DestroyEnemy(deadTime));
            }
        }
    }

    public void EnemyPush()
    {
        enemyRb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
        //Yönüne göre itilme ve rotasyon değişimi
        if (pushRotation < 0)
        {
            enemyRb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
            Enemy.transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
        else
        {
            enemyRb.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
            Enemy.transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }
    }

    //Belirlenen sürede objeyi yok etme
    IEnumerator DestroyEnemy(float seconds)
    {
        Enemy.GetComponent<Animator>().SetBool("IsDead", true);
        deadStarted = false;
        yield return new WaitForSeconds(seconds);
        if (this.gameObject.tag == "Boss")
        {
            gameOverScreen.SetActive(true);
            inGameScreen.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            Destroy(Enemy);
        }
    }
}
