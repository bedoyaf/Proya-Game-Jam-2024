using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    Vector3 originalPosition;
    public float despawnDistance = 5f;
    public Collider2D collider;
    public string layerOwner;
    public float spawnDistance=2f;
    void Start()
    {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("layer name" + collision.gameObject.layer);
        // Check if the collision is not with the Player or Bullet layer
        if (collision.gameObject.layer != LayerMask.NameToLayer(layerOwner) && collision.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            if (layerOwner == "Enemy")
            {
                Debug.Log("Its Enemy");
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();

                if (player != null)
                {
                    // Call the TakeDamage method
                    player.TakeDamage(5);
                }
                Destroy(gameObject);
                
            }
            else
            {
                // Get the component that contains the TakeDamage method
                EnemyDefault enemy = collision.gameObject.GetComponent<EnemyDefault>();

                // Check if the object has the EnemyDefault component
                if (enemy != null)
                {
                    // Call the TakeDamage method
                    enemy.TakeDamage(5);
                }
                Destroy(gameObject);
            }
        }
    }
}
