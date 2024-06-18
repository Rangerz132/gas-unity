using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public static Action OnTakeDamage;
    public static Action OnHeal;

    /// <summary>
    /// Deal damage to entity
    /// </summary>
    /// <param name="damageAmount"></param>
    public virtual void TakeDamage(GameObject damageDealer, float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        OnTakeDamage?.Invoke();
    }

    /// <summary>
    /// Heal entity
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void Heal(GameObject healDealer, float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnTakeDamage?.Invoke();
    }
}