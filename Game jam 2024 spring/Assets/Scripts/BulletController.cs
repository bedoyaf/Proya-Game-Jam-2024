using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    Vector3 originalPosition;
    public float despawnDistance = 10f;
    public Collider2D collider;
    public GameObject owner;
    public GameObject shootingEnemy;
    public GameObject meleeEnemy;
    public GameObject explosiveEnemy;
    void Start()
    {
    }

    // Update is called once per frame
    public float speed = 10f;

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, originalPosition);
        if (distance >= despawnDistance)
        {
            Destroy(gameObject);
        }
        // Debug.Log(speed);
    }

    public void SetDirection(Vector2 direction, Vector3 _originalPosition, float additionalbulletSpeed)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        direction = direction.normalized;
        speed += additionalbulletSpeed;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        originalPosition = _originalPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is not with the Player or Bullet layer
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            // Get the component that contains the TakeDamage method
            EnemyDefault enemy = collision.gameObject.GetComponent<EnemyDefault>();

            // Check if the object has the EnemyDefault component
            if (enemy != null)
            {

                int damageToDeal = 5;
                if (enemy.colour == "red")
                {
                    damageToDeal = 20;
                }
                if ((enemy.currentHealth-damageToDeal)<=0)
                {
                    if (enemy.colour == "green")
                    {
                        GameController.Instance.AddPoints(2, 1);
                    }
                    else if (enemy.colour == "red")
                    {
                        GameController.Instance.AddPoints(3, 1);
                    }
                    else if (enemy.colour == "purple")
                    {
                        GameController.Instance.AddPoints(1, 1);
                    }
                }

                // Call the TakeDamage method
                enemy.TakeDamage(damageToDeal);
            }


            Destroy(gameObject);
        }
    }
}