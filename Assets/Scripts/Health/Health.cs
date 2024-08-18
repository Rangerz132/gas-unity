using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float baseHealth;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public static Action OnTakeDamage;
    public static Action OnHeal;
    public static Action OnMaxHealChange;

    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
        InitializeHealth();
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
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }

        OnTakeDamage?.Invoke();
    }

    /// <summary>
    /// Heal entity
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void Heal(GameObject healDealer, float healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        OnTakeDamage?.Invoke();
    }

    private void HandleDerivedStatChanged(DerivedCharacterStatType statType, float newValue)
    {
        if (statType == DerivedCharacterStatType.MaxHealth)
        {
            UpdateMaxHealth();
        }
    }

    /// <summary>
    /// Initialize health values
    /// </summary>
    private void InitializeHealth()
    {
        UpdateMaxHealth();
        RestoreHealth();
    }

    /// <summary>
    /// Set max health based on character stats and base value
    /// </summary>
    private void UpdateMaxHealth()
    {
        float bonusHealth = characterStatsManager.GetDerivedStatValue(DerivedCharacterStatType.MaxHealth);
        MaxHealth = baseHealth + bonusHealth;
        CurrentHealth += bonusHealth;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);

        OnMaxHealChange?.Invoke();
    }

    /// <summary>
    /// Restore health to the maximum amount
    /// </summary>
    private void RestoreHealth()
    {
        CurrentHealth = MaxHealth;
    }
}