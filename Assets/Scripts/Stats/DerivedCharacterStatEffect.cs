using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DerivedCharacterStatType
{
    MaxHealth,
    MagicDamage,
    PhysicalDamage,
    Accuracy,
    Evasion,
    CriticalHitChance,
    ManaPool,
    StaminaPool,
    MovementSpeed,
    AttackSpeed,
    MagicResistance,
    PhysicalResistance,
    Regeneration,
    AbilityCooldown
}

[Serializable]
public class DerivedCharacterStatEffect
{
    public DerivedCharacterStatType derivedStatType;
    public float effectPerPoint;
}
