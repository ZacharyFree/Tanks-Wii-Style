using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    [Range(.5f,3f)]
    public float enemyAccuracy = .5f;
    [Range(15f,30f)]
    public float enemyTurnSpeed = 15f;
    public GameObject deathEffect;
    public GameObject enemyAmmo;
    public HealthBar healthBar;
    public GameObject player;
    public Transform EnemyTurret;
    public Collider2D detectionZone;
    public AudioClip enemyFire;

    private int timer;

    [HideInInspector] public bool canSeePlayer;//hidden because it will be accessed from "Detect Enemy script

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

        // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        Shoot();
    }

    public void TakeDamage(int damage)
    {
        //this function is referenced in the BulletBehavior script in the OnTriggerEnter function
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        //this.GetComponent<HealthBar>().SetMaxHealth(100);
        else
        {
            healthBar.SetHealth(health);
        }

    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DetectPlayer()
    {
        Vector3 playerPosition = player.transform.position;//These two lines get "Object reference not set to instance of object"  error
        Vector3 enemyPosition = gameObject.transform.position;
        Vector2 offset = new Vector2(enemyPosition.x - playerPosition.x, enemyPosition.y - playerPosition.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + 90f;
        EnemyTurret.GetComponentInParent<Transform>().transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //code to detect player in order to shoot at you
    }

    void Shoot()
    {

        if (canSeePlayer)
        {
            //Aim();
            if (timer == 0)
            {
                Instantiate(enemyAmmo, EnemyTurret.position, EnemyTurret.rotation);
                AudioSource.PlayClipAtPoint(enemyFire, EnemyTurret.position);
                //Recoil Animation;

                timer++;
            }
            else
            {
                if (timer >= 240)
                {
                    timer = 0;
                }
                else
                {
                    timer++;
                }
            }

        }

    }
    /*void Aim()
    {
        Vector3 enemyPosition = gameObject.transform.position;
        Vector3 playerPosition = player.transform.position;

    }*/
}
