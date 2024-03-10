using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    Vector3 originalPosition;
    public Collider2D collider;
    public SpriteRenderer spriteRenderer;
    public float ExplosionFadeTime = 2;
    public Sprite bomb;
    public Sprite Explosion;
    public float explosionTime = 2f;
    public float explosionRadius = 3f;
    public LayerMask explosionLayers;


    private bool exploded=false;
    private float elapsedTime = 0f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the countdown to explosion
        Invoke("Explode", explosionTime);
    }
    void Explode()
    {
       // yield return new WaitForSeconds(explosionTime);

        //Debug.Log("exploduji");
        spriteRenderer.sprite = Explosion;
        float newScale = (explosionRadius * 2) / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(newScale, newScale, 1f);
        // Perform explosion logic

        collider.isTrigger = true;
        exploded = true;
       // yield return new WaitForSeconds(ExplosionFadeTime);
        // Destroy the bomb
        //Destroy(gameObject);
    }
    void Update()
    {
        if (exploded)
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayers);

            foreach (Collider2D hitCollider in colliders)
            {
                // Handle the object hit in the explosion (e.g., damage, destroy, etc.)
                // Add your custom logic based on the hit object's type
                if(hitCollider.gameObject !=gameObject && (hitCollider.CompareTag("Enemy")|| hitCollider.CompareTag("Player")))
                {

                    // ADD PLAYER DAMAGE 
                    PlayerController player = hitCollider.gameObject.GetComponent<PlayerController>();

                    if (player != null)
                    {
                        // Call the TakeDamage method
                        player.TakeDamage(10);
                    }


                    EnemyDefault enemy = hitCollider.gameObject.GetComponent<EnemyDefault>();

                    // Check if the object has the EnemyDefault component
                    if (enemy != null)
                    {
                        // Call the TakeDamage method
                        enemy.TakeDamage(10);
                    }
                  //  Debug.Log("Hit: " + hitCollider.gameObject.name);
                }
            }
            // Perform fading or other actions based on elapsed time

            // For example, changing alpha value for a sprite renderer:
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / ExplosionFadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

          //  transform.up += 0.0000001;

            // Check if the fading duration has been reached
            if (elapsedTime >= ExplosionFadeTime)
            {
                // Reset variables or perform any final actions
                exploded = false;
                elapsedTime = 0f;

                // Destroy the bomb or perform any other cleanup
                Destroy(gameObject);
            }
        }
    }
   /* void OnTriggerEnter2D(Collider2D other)
    {
        if(exploded)
        {
            Debug.Log("Entered trigger: " + other.gameObject.name);
        }
        // Handle the object that entered the trigger area

        // Handle the object hit in the explosion (e.g., damage, destroy, etc.)
        // Add your custom logic based on the hit object's type
        /*if (other.CompareTag("Enemy"))
        {
            // Handle damage to enemies
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player") )
        {
            // Handle damage to players (excluding the bomb owner)
            Destroy(other.gameObject);
        }*/

        // Optionally, disable the collider to prevent multiple triggers
        // GetComponent<Collider2D>().enabled = false;
    //}
}
