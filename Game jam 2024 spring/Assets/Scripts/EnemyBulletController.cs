using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet: MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    Vector3 originalPosition;
    public float despawnDistance = 10f;
    public Collider2D collider;
    public string layerName;
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

    public void SetLayerName(string newLayerName)
    {
        layerName = newLayerName;
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
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy") && collision.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                // Call the TakeDamage method
                player.TakeDamage(5);
            }
            // Destroy the bullet on collision with anything other than the Player layer
            Destroy(gameObject);
       }
    }
}
