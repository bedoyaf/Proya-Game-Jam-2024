using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Declare an array to hold Animator Controllers

    public float health = 100;
    public HealthBarController healthbar;
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public GameObject meleePrefab;
    public GameObject bombPrefab;
    public SpriteRenderer spriteRenderer;
    [SerializeField] GameObject gameOverCanvas;
    Animator animator ;
    private bool idle=true;
    private bool showMenu = false;
    public string colour = string.Empty;

    public float delayBetweenShots = 0.5f; // Adjust this value to set the delay between shots
    private float lastShotTime;
    void Start()
    {
        rb.freezeRotation = true;
        animator= GetComponent<Animator>();

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        healthbar.SetHealth(health);
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //  Vector2 movement = new Vector2(horizontal, vertical);
        //  movement.Normalize(); // Ensure diagonal movement isn't faster
        //  rb.velocity = movement * speed;

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;
        MovePlayer(moveDirection);
        FlipSprite(horizontal);
     //   float currentspeed = moveDirection.magnitude;
      //  float speed = moveDirection.magnitude;
    //    animator.SetFloat("Speed", currentspeed);

        bool isMoving = Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0;
          if (isMoving)
          {
              animator.SetBool("running",true);
          }
          else 
          {
            //Debug.Log("nehybu se");
              animator.SetBool("running",false);
          }
        
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your preferred input axis
        {
            if (Time.time - lastShotTime > delayBetweenShots)
            {
                // Your shooting logic goes here
                Shoot();
                // Update the last shot time
                lastShotTime = Time.time;
            }
        }
        if (Input.GetButtonDown("Fire2")) // Change "Fire1" to your preferred input axis
        {
            if (Time.time - lastShotTime > delayBetweenShots)
            {
                // Your shooting logic goes here
                Melee();
                // Update the last shot time
                lastShotTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastShotTime > delayBetweenShots)
            {
                // Your shooting logic goes here
                DropBomb();
                // Update the last shot time
                lastShotTime = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("im here");
            showMenu = !showMenu;
            gameOverCanvas.SetActive(showMenu);
            foreach (Transform child in gameOverCanvas.transform)
            {
                if (child.name != "GameOverText")
                {
                    child.gameObject.SetActive(showMenu);
                }
            }
            
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage (int damage)
    {
        if (health - damage <= 0)
        {
            Die();
            health = 0;
            gameOverCanvas.SetActive(true);
        }
        else
        {
            health -= damage;
        }
        healthbar.SetHealth(health);
      //  Debug.Log("Player:"+health);
    }

    void MovePlayer(Vector2 moveDirection)
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }
    void Shoot()
    {
        if (colour == "purple" || colour == string.Empty)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 shootDirection = (mousePosition - transform.position).normalized;

            // Get the player's Rigidbody2D component (assuming the player has one)

            // Calculate the bullet speed by combining the bullet's speed and the player's speed
            // float dotProduct = Vector2.Dot(shootDirection, rb.velocity);
            float bulletSpeed = rb.velocity.magnitude;//*dotProduct;
                                                      // Debug.Log(bulletSpeed);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 currentPosition = transform.position;

            // Set the bullet's direction and speed
            bullet.GetComponent<BulletController>().SetDirection(shootDirection, currentPosition, bulletSpeed);
        }
    }
    void Melee()
    {
        if (colour == "green" || colour==string.Empty)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 meleeDirection = (mousePosition - transform.position).normalized;
            Vector3 spawnPosition = transform.position + new Vector3(meleeDirection.x, meleeDirection.y, 0f);//* meleeDistance;
            float bulletSpeed = rb.velocity.magnitude;//*dotProduct;
                                                      // Instantiate the melee object at the calculated position
            GameObject meleeObject = Instantiate(meleePrefab, transform.position, Quaternion.identity);
            Vector3 currentPosition = transform.position;
            // Set the direction and speed if your melee object has a script for that
            meleeObject.GetComponent<MeleeController>().SetDirection(meleeDirection, currentPosition, meleeDirection, bulletSpeed);
        }
    }
    void DropBomb()
    {
        if (colour == "red" || colour == string.Empty)
        {
            // Instantiate a bomb prefab at the player's position
            Vector3 playerPosition = transform.position;
            Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y + 0.000001f, playerPosition.z);
            GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

            // Set the bomb owner (optional, if your BombController script uses it)
            //bomb.GetComponent<BombController>().SetOwner(gameObject);
        }
    }
    void FlipSprite(float horizontal)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (horizontal < 0)
        {
            // Moving left, flip the sprite
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            // Moving right, restore the sprite to its original orientation
            spriteRenderer.flipX = false;
        }
        // If horizontal is 0, the player is not moving left or right, so don't change the sprite orientation.
    }

    public void ChangeSpriteColor(Color newColor)
    {
        spriteRenderer.color = newColor; // Set the new color to the SpriteRenderer component
    }
}
