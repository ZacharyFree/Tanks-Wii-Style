using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankPlayerController : MonoBehaviour
{
    //VARIABLES===================================================================================================
    public float speed = 2f;
    private bool continuousShot;
    public int maxLiveBullets = 5;
    public int health = 100;
    [Header("References")]
    public new Rigidbody2D rigidbody;
    //public Animator trackAnimation;
    public GameObject[] bullet; //select bullet sprite
    public Transform firePoint;
    public Transform TurretAim;
    public Transform TankTracks;
    public AudioClip[] plazmaBlast;
    public GameObject deathAnimation;
   
    public Dropdown bulletSelection;
    private GameObject bulletOfChoice;
    private AudioClip soundOfChoice;

    /*[Range(.5f, 3f)]
    public float playerTrackTurnAccuracy;
    [Range(15f, 30f)]
    public float playerTrackTurnSpeed;*/

    public HealthBar healthBar;

    private Camera mainCam;

    //private float angle;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        //trackAnimation = GetComponent<Animator>();
        //bullet = GetComponent<GameObject>();
        continuousShot = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Movement();
        BulletOfChoice();
        Shoot();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        healthBar.slider.value = health;

        if (health <= 0)
        {
            Instantiate(deathAnimation, gameObject.transform);
            Destroy(gameObject);
            
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Movement()
    {
        float GoingForward = Input.GetAxis("Vertical");
        float GoingSideways = Input.GetAxis("Horizontal");

        if (GoingForward != 0f || GoingSideways != 0f)
        {
            //trackAnimation.SetBool("isMoving", true);
            transform.Translate(Vector3.up * GoingForward * speed * .1f);
            transform.Translate(Vector3.right * GoingSideways * speed * .1f);
        }
        else
        {
            //trackAnimation.SetBool("isMoving", false);
        }

        //following if sequence turns the tracks to face the direction the tank is traveling
        if (GoingForward > 0f/* i.e. the UP arrow */)
        {
            /*angle = 0f;
            while (angle <= playerTrackTurnAccuracy)
            {
                TankTracks.rotation = Quaternion.Euler(0f, 0f, angle / playerTrackTurnSpeed);
                angle -= angle / playerTrackTurnSpeed;
            }*/
            TankTracks.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (GoingForward < 0f /* i.e. the DOWN arrow */)
        {
            //angle = 180f;
            TankTracks.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (GoingSideways > 0f /* i.e. the RIGHT arrow */)
        {
            //angle = 90f;
            TankTracks.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if( GoingSideways < 0f /*i.e. the LEFT arrow */)
        {
            //angle = -90f;
            TankTracks.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }

    //Recieve damage from enemy fire
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("projectile"))
        {
            int bulletTypeDamageAmount = other.GetComponent<BulletBehavior>().damage;
            health -= bulletTypeDamageAmount; 
            
        }
    }

    void Shoot()
    {
        if (continuousShot)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(bulletOfChoice, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(soundOfChoice, firePoint.position);
            }
        }
        else
        {
            int actualLiveBullets = GameObject.FindGameObjectsWithTag("Player").Length;
            if (Input.GetButtonDown("Fire1"))
            {
                if (actualLiveBullets < maxLiveBullets)
                {
                    Instantiate(bulletOfChoice, firePoint.position, firePoint.rotation);
                    AudioSource.PlayClipAtPoint(soundOfChoice, firePoint.position);
                }
            }
        }
        
    }
    void Aim()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = mainCam.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg - 90f;
        TurretAim.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void SetContinuousShotBoolInOptionsMenu()//must be public so that the UI component shows it as an option in it's list of functions
    {
        if (continuousShot)
        {
            continuousShot = false;
        }
        else 
        { 
            continuousShot = true;
        } 
    }

    void BulletOfChoice()
    {
        int menuIndex = bulletSelection.value;//assigns an integer to each item in our dropdown list counting from 0
        bulletOfChoice = bullet[menuIndex];//selects the gameobject from our array of bullet gameobjects based on our dropdown selection
        soundOfChoice = plazmaBlast[menuIndex];//selects the audio clip from our array based on our dropdown selection
    }
}
