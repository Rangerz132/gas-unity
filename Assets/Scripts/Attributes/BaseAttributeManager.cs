using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttributeManager : MonoBehaviour
{
    [SerializeField] protected float baseAttribute;
    [SerializeField] protected float baseAttributeRegen;
    public float CurrentAttribute { get; protected set; }
    public float CurrentAttributeRegen { get; protected set; }
    public float MaxAttribute { get; private set; }

    public static Action OnMaxAttributeChange;

    protected CharacterStatsManager characterStatsManager;
    protected DerivedCharacterStatType derivedCharacterStatType;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
        InitializeAttribute();
    }

    /// <summary>
    /// Update max attribute on stat change
    /// </summary>
    /// <param name="statType"></param>
    /// <param name="newValue"></param>
    protected void HandleDerivedStatChanged(DerivedCharacterStatType statType, float newValue)
    {
        if (statType == derivedCharacterStatType)
        {
            UpdateMaxAttribute();
        }
    }

    /// <summary>
    /// Initialize attribute values
    /// </summary>
    protected void InitializeAttribute()
    {
        CurrentAttributeRegen = baseAttributeRegen;
        UpdateMaxAttribute();
        RestoreAttribute();
    }

    /// <summary>
    /// Set max attribute based on character stats and base value
    /// </summary>
    protected void UpdateMaxAttribute()
    {
        float bonusAttribute = characterStatsManager.GetDerivedStatValue(derivedCharacterStatType);
        MaxAttribute = baseAttribute + bonusAttribute;
        CurrentAttribute += bonusAttribute;
        CurrentAttribute = Mathf.Min(CurrentAttribute, MaxAttribute);

        OnMaxAttributeChange?.Invoke();
    }

    /// <summary>
    /// Restore attribute to the maximum amount
    /// </summary>
    protected void RestoreAttribute()
    {
        CurrentAttribute = MaxAttribute;
    }
}
