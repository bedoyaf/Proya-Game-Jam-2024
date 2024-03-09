using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public GameObject meleePrefab;
    void Start()
    {
        rb.freezeRotation = true;
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

        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your preferred input axis
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2")) // Change "Fire1" to your preferred input axis
        {
            Melee();
        }

    }

    void MovePlayer(Vector2 moveDirection)
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    /*    RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 0.1f, LayerMask.GetMask("Walls"));

        // If there's no collision with the Tilemap, move the player
        if (hit.collider == null || !hit.collider.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }
        else
        {
            Debug.Log("Hit layer: " + hit.collider.gameObject.layer);
            Debug.Log("Hit tag: " + hit.collider.tag);
            // Handle collision with the wall (optional)
            // For example, stop the player or play a sound
            rb.velocity = Vector2.zero;
        }*/
    }
    void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        // Get the player's Rigidbody2D component (assuming the player has one)

        // Calculate the bullet speed by combining the bullet's speed and the player's speed
        // float dotProduct = Vector2.Dot(shootDirection, rb.velocity);
        float bulletSpeed = rb.velocity.magnitude;//*dotProduct;
        Debug.Log(bulletSpeed);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 currentPosition = transform.position;

        // Set the bullet's direction and speed
        bullet.GetComponent<BulletController>().SetDirection(shootDirection, currentPosition, bulletSpeed);
    }
    void Melee()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 meleeDirection = (mousePosition - transform.position).normalized;

        // Set the distance you want the melee object to be spawned from the player
        //float meleeDistance = 1.0f; // Adjust this value as needed

        // Calculate the position where the melee object should be spawned
        Vector3 spawnPosition = transform.position + new Vector3(meleeDirection.x, meleeDirection.y, 0f);//* meleeDistance;
        float bulletSpeed = rb.velocity.magnitude;//*dotProduct;
        // Instantiate the melee object at the calculated position
        GameObject meleeObject = Instantiate(meleePrefab, transform.position, Quaternion.identity);
        Vector3 currentPosition = transform.position;
        // Set the direction and speed if your melee object has a script for that
        meleeObject.GetComponent<MeleeController>().SetDirection(meleeDirection,currentPosition,meleeDirection,bulletSpeed);
    }
}
