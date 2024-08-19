using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : BaseAttributeManager
{
    public static Action OnAttributeConsume;
    public static Action OnAttributeRegen;

    private void Start()
    {
        derivedCharacterStatType = DerivedCharacterStatType.MaxStamina;
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
    /// Consume stamina
    /// </summary>
    /// <param name="staminaCost"></param>
    public void ConsumeStamina(float staminaCost)
    {
        CurrentAttribute -= staminaCost;

        if (CurrentAttribute < 0)
        {
            CurrentAttribute = 0;
        }

        OnAttributeConsume?.Invoke();
    }

    /// <summary>
    /// Regen stamina over time
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
