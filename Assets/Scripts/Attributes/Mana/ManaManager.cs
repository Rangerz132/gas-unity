using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : BaseAttributeManager
{
    public static Action OnAttributeConsume;
    public static Action OnAttributeRegen;

    private void Start()
    {
        derivedCharacterStatType = DerivedCharacterStatType.MaxMana;
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
        if (CurrentAttribute < MaxAttribute)
        {
            RegenOverTime();
        }
    }

    /// <summary>
    /// Consume Mana
    /// </summary>
    /// <param name="manaCost"></param>
    public void ConsumeMana(float manaCost)
    {
        CurrentAttribute -= manaCost;

        if (CurrentAttribute < 0)
        {
            CurrentAttribute = 0;
        }

        OnAttributeConsume?.Invoke();
    }

    /// <summary>
    /// Regen Mana over time
    /// </summary>
    private void RegenOverTime()
    {
        CurrentAttribute += Time.deltaTime * CurrentAttributeRegen;

        if (CurrentAttribute > MaxAttribute)
        {
            CurrentAttribute = MaxAttribute;
        }
        else
        {
            OnAttributeRegen?.Invoke();
        }
    }
}
