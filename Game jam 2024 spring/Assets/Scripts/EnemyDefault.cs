using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyDefault : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    public int currentHealth;
    public string colour = "white";
    [SerializeField] protected Slider healthBarSlider;
    public Transform target;
    public bool isIdling = true;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthBar();
        Debug.Log(healthBarSlider.value);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool HasObstaclesInFrontOfEnemy()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        direction.z = 0;
        LayerMask layerMask = LayerMask.GetMask("Player", "Walls");
        RaycastHit2D hit2D = Physics2D.Raycast(currentPosition, direction, 100, layerMask);

        if (hit2D.collider != null && !hit2D.collider.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = (float)currentHealth / maxHealth;
        }
    }
}
