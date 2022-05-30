using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    EnemyController enemyController;
    public GameObject boss;
    [SerializeField] float lifeTime;
    public bool isEnemyBullet = false;
    private Vector2 lastPos;
    private Vector2 currentPos;
    private Vector2 playerPos;
    bool wasShot = false;
    int waitTime = 2;

    void Start()
    {
        StartCoroutine(DeathDelay());
        StartCoroutine(LateStart());
        if(!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
        
    }

    IEnumerator LateStart()
     {
         yield return new WaitForSeconds(waitTime);
         enemyController = boss.GetComponent<EnemyController>();
     }
    
    void Update()
    {
        if(isEnemyBullet)
        {
            currentPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if(currentPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = currentPos;
        }
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy" && !isEnemyBullet)
        {
            col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if(col.tag == "Boss" && !isEnemyBullet)
        {
            enemyController.BossHealth -= 1;
            Destroy(gameObject);
            if(enemyController.BossHealth <= 0)
            {
                col.gameObject.GetComponent<EnemyController>().Death();
            }
        }

        if(col.tag == "Player" && isEnemyBullet && !wasShot)
        {
            wasShot = true;
            GameController.DamagePlayer(1);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
