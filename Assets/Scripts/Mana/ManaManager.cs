using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private float baseMana;
    [SerializeField] private float baseManaRegen;
    public float CurrentMana { get; private set; }
    public float CurrentManaRegen { get; private set; }
    public float MaxMana { get; private set; }

    public static Action OnManaConsume;
    public static Action OnManaRegen;
    public static Action OnMaxManaChange;

    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
        InitializeMana();
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
        if (CurrentMana < MaxMana)
        {
            RegenOverTime();
        }
    }

    /// <summary>
    /// Consume mana
    /// </summary>
    /// <param name="manaCost"></param>
    public void ConsumeMana(float manaCost)
    {
        CurrentMana -= manaCost;

        if (CurrentMana < 0)
        {
            CurrentMana = 0;
        }

        OnManaConsume?.Invoke();
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
            UpdateMaxMana();
        }
    }

    /// <summary>
    /// Initialize mana values
    /// </summary>
    private void InitializeMana()
    {
        CurrentManaRegen = baseManaRegen;
        UpdateMaxMana();
        RestoreMana();
    }

    /// <summary>
    /// Set max mana based on character stats and base value
    /// </summary>
    private void UpdateMaxMana()
    {
        float bonusMana = characterStatsManager.GetDerivedStatValue(DerivedCharacterStatType.MaxMana);
        MaxMana = baseMana + bonusMana;
        CurrentMana += bonusMana;
        CurrentMana = Mathf.Min(CurrentMana, MaxMana);

        OnMaxManaChange?.Invoke();
    }

    /// <summary>
    /// Restore mana to the maximum amount
    /// </summary>
    private void RestoreMana()
    {
        CurrentMana = MaxMana;
    }

    /// <summary>
    /// Regen Mana over time
    /// </summary>
    private void RegenOverTime()
    {
        CurrentMana += Time.deltaTime * CurrentManaRegen;

        if (CurrentMana > MaxMana)
        {
            CurrentMana = MaxMana;
        }
        else
        {
            OnManaRegen?.Invoke();
        }
    }
}
