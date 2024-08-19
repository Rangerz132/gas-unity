using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private float baseStamina;
    [SerializeField] private float baseStaminaRegen;
    public float CurrentStamina { get; private set; }
    public float CurrentStaminaRegen { get; private set; }
    public float MaxStamina { get; private set; }

    public static Action OnStaminaConsume;
    public static Action OnStaminaRegen;
    public static Action OnMaxStaminaChange;

    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
        InitializeStamina();
    }

    private void OnEnable()
    {
        characterStatsManager.OnDerivedStatChanged += HandleDerivedStatChanged;
    }

    private void OnDisable()
    {
        characterStatsManager.OnDerivedStatChanged -= HandleDerivedStatChanged;
    }

    void Update()
    {
        if (CurrentStamina < MaxStamina)
        {
            RegenOverTime();
        }
    }

    /// <summary>
    /// Consume stamina
    /// </summary>
    /// <param name="staminaCost"></param>
    public void ConsumeStamina(float staminaCost)
    {
        CurrentStamina -= staminaCost;

        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }

        OnStaminaConsume?.Invoke();
    }

    /// <summary>
    /// Update Max Health on Stat Changed
    /// </summary>
    /// <param name="statType"></param>
    /// <param name="newValue"></param>
    private void HandleDerivedStatChanged(DerivedCharacterStatType statType, float newValue)
    {
        if (statType == DerivedCharacterStatType.MaxMana)
        {
            UpdateMaxStamina();
        }
    }

    /// <summary>
    /// Initialize stamina values
    /// </summary>
    private void InitializeStamina()
    {
        CurrentStaminaRegen = baseStaminaRegen;
        UpdateMaxStamina();
        RestoreStamina();
    }

    /// <summary>
    /// Set max stamina based on character stats and base value
    /// </summary>
    private void UpdateMaxStamina()
    {
        float bonusStamina = characterStatsManager.GetDerivedStatValue(DerivedCharacterStatType.MaxStamina);
        MaxStamina = baseStamina + bonusStamina;
        CurrentStamina += bonusStamina;
        CurrentStamina = Mathf.Min(CurrentStamina, MaxStamina);

        OnMaxStaminaChange?.Invoke();
    }

    /// <summary>
    /// Restore stamina to the maximum amount
    /// </summary>
    private void RestoreStamina()
    {
        CurrentStamina = MaxStamina;
    }

    /// <summary>
    /// Regen Mana over time
    /// </summary>
    private void RegenOverTime()
    {
        CurrentStamina += Time.deltaTime * CurrentStaminaRegen;

        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        else
        {
            OnStaminaRegen?.Invoke();
        }
    }
}
