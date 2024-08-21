using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DerivedCharacterStatType
{
    MaxHealth, 
    MaxMana,
    MaxStamina,
    MagicDamage,
    PhysicalDamage, 
    Accuracy,
    Evasion,
    CriticalHitChance,
    MovementSpeed,
    AttackSpeed,
    MagicResistance, 
    PhysicalResistance, 
    HealthRegeneration,
    StaminaRegeneration,
    ManaRegeneration,
    AbilityCooldown, 
    MaxWeight
}

[CreateAssetMenu(fileName = "DerivedCharacterStatEffect", menuName = "Stats/DerivedCharacterStatEffect", order = 0)]
public class DerivedCharacterStatEffect: ScriptableObject
{
    public DerivedCharacterStatType derivedStatType;
    public float effectPerPoint;
    [TextArea] public string description;
}
