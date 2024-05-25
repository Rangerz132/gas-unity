using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    /// <summary>
    /// Deal damage to entity
    /// </summary>
    /// <param name="damageAmount"></param>
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    /// <summary>
    /// Heal entity
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}