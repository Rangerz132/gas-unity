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

[Serializable]
public class DerivedCharacterStatEffect
{
    public DerivedCharacterStatType derivedStatType;
    public float effectPerPoint;
}
