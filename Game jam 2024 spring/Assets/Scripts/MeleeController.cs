using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Source;
    public AudioClip MeleeClip;
    public Rigidbody2D rb;
    Vector3 originalPosition;
    public float despawnDistance = 5f;
    public Collider2D collider;
    public string layerOwner;
    public float spawnDistance=2f;
    void Start()
    {
        Source.clip = MeleeClip;
        Source.Play();
    }

    // Update is called once per frame
    public float speed = 9f;

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, originalPosition);
        if (distance >= despawnDistance)
        {
            Destroy(gameObject);
        }
     //   Debug.Log(speed);
    }

    public void SetDirection(Vector2 direction, Vector3 _originalPosition, Vector3 newposition,float additionalbulletSpeed)
    {
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //   transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += -90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.transform.position = rb.transform.position+newposition*spawnDistance;
        direction = direction.normalized;
        speed += additionalbulletSpeed;
      //  Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        originalPosition = _originalPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Trigger entered?");
        // Debug.Log("layer name" + other.gameObject.layer);

        // Check if the trigger is not with the Player or Bullet layer
        if (other.gameObject.layer != LayerMask.NameToLayer(layerOwner))
        {
            if (layerOwner == "Enemy")
            {
                // Debug.Log("It's Enemy");
                PlayerController player = other.gameObject.GetComponent<PlayerController>();

                if (player != null)
                {
                    // Call the TakeDamage method
                    player.TakeDamage(5);
                }
                // Destroy(gameObject);
            }
            else
            {
               // Debug.Log("I shot and I'm a player");
                // Get the component that contains the TakeDamage method
                EnemyDefault enemy = other.gameObject.GetComponent<EnemyDefault>();

                // Check if the object has the EnemyDefault component
                if (enemy != null)
                {
                    int damageToDeal = 5;
                    if (enemy.colour == "purple")
                    {
                        damageToDeal = 20;
                    }

                    if ((enemy.currentHealth - damageToDeal) <= 0)
                    {
                        if (enemy.colour == "purple")
                        {
                            GameController.Instance.AddPoints(2, 1);
                        }
                        else if (enemy.colour == "red")
                        {
                            GameController.Instance.AddPoints(3, 1);
                        }
                        else if (enemy.colour == "green")
                        {
                            GameController.Instance.AddPoints(1, 1);
                        }
                    }

                    // Call the TakeDamage method
                    enemy.TakeDamage(damageToDeal);
                    // Destroy(gameObject);
                }
                // Destroy(gameObject);
            }
        }
    }
}
