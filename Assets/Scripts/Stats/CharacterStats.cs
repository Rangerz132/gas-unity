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
    public CharacterStatType type;
    public Stat stat;
}

public class CharacterStats : MonoBehaviour
{
    // Stats
    [SerializeField] private List<CharacterStatInfo> statInfos;
    public Dictionary<CharacterStatType, Stat> Stats { get; private set; }

    // Stat Effects
    private List<IStatEffect> statEffects = new List<IStatEffect>();
    private Dictionary<DerivedCharacterStatType, float> derivedStats = new Dictionary<DerivedCharacterStatType, float>();
    public event Action<DerivedCharacterStatType, float> OnDerivedStatChanged;


    void Awake()
    {
        InitializeStats();
        InitializeEffects();


        foreach (CharacterStatInfo info in statInfos)
        {
            info.stat.OnBaseValueChanged += ApplyAllEffects;
            info.stat.OnValueChanged += ApplyAllEffects;
        }
    }

    private void InitializeStats()
    {
        Stats = new Dictionary<CharacterStatType, Stat>();

        foreach (CharacterStatInfo info in statInfos)
        {
            if (!Stats.ContainsKey(info.type))
            {
                Stats.Add(info.type, info.stat);
            }
        }
    }

    private void InitializeEffects()
    {
        statEffects.Add(new ConstitutionMaxHealthEffect(10f));
        statEffects.Add(new WisdomCooldownAbilityEffect(0.5f));
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
            characterStatInfo.type = statType;
            characterStatInfo.stat = newStat;
            Stats.Add(characterStatInfo.type, characterStatInfo.stat);

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
        foreach (var effect in statEffects)
        {
            effect.ApplyEffect(this);
        }
    }
}
