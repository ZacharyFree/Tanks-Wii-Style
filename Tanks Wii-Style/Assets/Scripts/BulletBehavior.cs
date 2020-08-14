using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb; //is the rigidbody of the bullet itself
    public float range = 2f;
    public int damage = 20;
    public GameObject impactBlast;
    public AudioClip crackle;
    public bool hasTrail = false;
    public GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Quaternion strikeQuaternion = new Quaternion();//arbitrary quaternion to satisfy our instantiate calls
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            AudioSource.PlayClipAtPoint(crackle, hitInfo.transform.position);
            /*animation play*/
            Destroy(gameObject); //stops bullet from passing through the enemy tank
            enemy.TakeDamage(damage); //comes from Enemy Script, which references HealthBar script;;;subtracts from health, then checks if health <= 0. If yes, calls Die() function
            Instantiate(impactBlast,hitInfo.transform);
            //No need to destroy the clone here, because the instantiated object has a script pre-attached that self-destructs
        }
        if (hitInfo.gameObject.CompareTag("Crates"))
        {
            AudioSource.PlayClipAtPoint(crackle, rb.transform.position);

            Instantiate(impactBlast, rb.position, strikeQuaternion);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //add trailing flames to bullets
        if (hasTrail)
        {
            Instantiate(trail, rb.transform);
        }
        //Destroy bullet after it leaves its range
        Destroy(gameObject, range);
    }
}
