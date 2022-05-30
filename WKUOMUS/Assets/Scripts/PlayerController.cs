using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float runSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;
    public TextMeshProUGUI collectedText;
    public static int collectedAmount = 0;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    Rigidbody2D myRigidbody;

    public bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        collectedText.text = collectedAmount.ToString();
    }

    void Update()
    {
        if(!isAlive) { return; }
        fireDelay = GameController.FireRate;
        runSpeed = GameController.MoveSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");
        if((shootHor != 0 || shootVer != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;
        }

        myRigidbody.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        //collectedText.SetText($"Items collected: " + collectedAmount); not being used currently
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed
        );
    }
}

