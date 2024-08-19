using UnityEngine;
using System;

public class HealthManager : BaseAttributeManager
{
    public static Action OnTakeDamage;
    public static Action OnHeal;

    private void Start()
    {
        derivedCharacterStatType = DerivedCharacterStatType.MaxHealth;
    }

    private void OnEnable()
    {
        characterStatsManager.OnDerivedStatChanged += HandleDerivedStatChanged;
    }

    private void OnDisable()
    {
        characterStatsManager.OnDerivedStatChanged -= HandleDerivedStatChanged;
    }


    /// <summary>
    /// Deal damage to entity
    /// </summary>
    /// <param name="damageAmount"></param>
    public virtual void TakeDamage(GameObject damageDealer, float damageAmount)
    {
        CurrentAttribute -= damageAmount;
        if (CurrentAttribute <= 0)
        {
            CurrentAttribute = 0;
        }

        OnTakeDamage?.Invoke();
    }

    /// <summary>
    /// Heal entity
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void Heal(GameObject healDealer, float healAmount)
    {
        CurrentAttribute += healAmount;
        if (CurrentAttribute > MaxAttribute)
        {
            CurrentAttribute = MaxAttribute;
        }

        OnTakeDamage?.Invoke();
    }
}