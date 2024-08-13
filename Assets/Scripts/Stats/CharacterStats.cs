using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterStatType
{
    Constitution,
    Endurance,
    Strength,
    Dexterity,
    Intelligence,
    Luck,
    Charisma,
    Wisdom
}

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
public class CharacterStatInfo
{
    public CharacterStatType statType;
    public Stat stat;
    public List<DerivedCharacterStatEffect> derivedStatEffects = new List<DerivedCharacterStatEffect>();
}

[Serializable]
public class DerivedCharacterStatEffect
{
    public DerivedCharacterStatType derivedStatType;
    public float effectPerPoint;
}

public class CharacterStats : MonoBehaviour
{
    // Stats
    [field: SerializeField] public List<CharacterStatInfo> StatInfos { get; private set; }
    public Dictionary<CharacterStatType, Stat> Stats { get; private set; }

    // Stat Effects
    private Dictionary<DerivedCharacterStatType, float> derivedStats = new Dictionary<DerivedCharacterStatType, float>();
    public event Action<DerivedCharacterStatType, float> OnDerivedStatChanged;


    void Awake()
    {
        InitializeStats();

        foreach (CharacterStatInfo info in StatInfos)
        {
            info.stat.OnBaseValueChanged += ApplyAllEffects;
            info.stat.OnValueChanged += ApplyAllEffects;
        }
    }

    private void InitializeStats()
    {
        Stats = new Dictionary<CharacterStatType, Stat>();

        foreach (CharacterStatInfo info in StatInfos)
        {
            if (!Stats.ContainsKey(info.statType))
            {
                Stats.Add(info.statType, info.stat);
            }
        }
    }


    public float GetStatValue(CharacterStatType statType)
    {
        if (Stats.TryGetValue(statType, out Stat stat))
        {
            return stat.Value;
        }
        else
        {
            Stat newStat = new Stat();
            CharacterStatInfo characterStatInfo = new CharacterStatInfo();
            characterStatInfo.statType = statType;
            characterStatInfo.stat = newStat;
            Stats.Add(characterStatInfo.statType, characterStatInfo.stat);

            return newStat.Value;
        }
    }

    public float GetDerivedStatValue(DerivedCharacterStatType statType)
    {
        return derivedStats.TryGetValue(statType, out float value) ? value : 0f;
    }

    public void SetDerivedStatValue(DerivedCharacterStatType statType, float value)
    {
        if (derivedStats.ContainsKey(statType))
        {
            derivedStats[statType] = value;
        }
        else
        {
            derivedStats.Add(statType, value);
        }

        OnDerivedStatChanged?.Invoke(statType, value);
    }

    public void ApplyAllEffects()
    {
        foreach (var statInfo in StatInfos)
        {
            foreach (var effect in statInfo.derivedStatEffects)
            {
                ApplyEffect(statInfo, effect);
            }
        }
    }

    private void ApplyEffect(CharacterStatInfo statInfo, DerivedCharacterStatEffect effect)
    {
        float statValue = statInfo.stat.Value;
        float bonus = statValue * effect.effectPerPoint;
        SetDerivedStatValue(effect.derivedStatType, bonus);
    }
}
