using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int Respawn = 0;
    public static GameController instance;
    private static float health = 6;
    private static float maxHealth = 6;
    private static float moveSpeed = 8f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    /* for combination power ups
    private bool bootCollected = false;
    private bool nerfGunCollected = false;
    
    public List<string> collectedNames = new List<string>(); 
    */

    public static float Health { get => health; set => health = value; }
    public static float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public TextMeshProUGUI healthText;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        healthText.text = health.ToString();    
    }

    /*
    void Update()
    {
        healthText.SetText($"Health: " + health);
    }
    */

    public static void Restart()
    {
        SceneManager.LoadScene(Respawn);
    }

    public static void DamagePlayer(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Restart();
            Health = 6;
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void AttackRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    /* for combination power ups
    public void UpdateCollectedItems(CollectionController item)
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames)
        {
            switch(i)
            {
                case "Boot":
                    bootCollected = true;
                break;
                case "Nerf Gun":
                    nerfGunCollected = true;
                break;
            }
        }

        if(bootCollected && nerfGunCollected)
        {
            AttackRateChange(0.25f);
        }
    }
    */
}
