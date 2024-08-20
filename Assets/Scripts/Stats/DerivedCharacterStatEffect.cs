using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DerivedCharacterStatType
{
    MaxHealth, // Done
    MaxMana,
    MaxStamina,
    MagicDamage, // Done
    PhysicalDamage, // Done
    Accuracy,
    Evasion,
    CriticalHitChance,
    MovementSpeed,
    AttackSpeed,
    MagicResistance, // Done
    PhysicalResistance, // Done
    HealthRegeneration, // Done
    StaminaRegeneration,
    ManaRegeneration,
    AbilityCooldown, // Done
    MaxWeight
}

[CreateAssetMenu(fileName = "DerivedCharacterStatEffect", menuName = "Stats/DerivedCharacterStatEffect", order = 0)]
public class DerivedCharacterStatEffect: ScriptableObject
{
    public DerivedCharacterStatType derivedStatType;
    public float effectPerPoint;
    [TextArea] public string description;
}
