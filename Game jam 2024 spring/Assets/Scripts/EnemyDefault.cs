using UnityEngine;
using UnityEngine.UI;

public class EnemyDefault : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;
    [SerializeField] protected Slider healthBarSlider;

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
