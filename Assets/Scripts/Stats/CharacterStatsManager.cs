using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStatsManager : MonoBehaviour
{
    [field: SerializeField] public CharacterStat Constitution { get; private set; }
    [field: SerializeField] public CharacterStat Endurance { get; private set; }
    [field: SerializeField] public CharacterStat Strength { get; private set; }
    [field: SerializeField] public CharacterStat Dexterity { get; private set; }
    [field: SerializeField] public CharacterStat Intelligence { get; private set; }
    [field: SerializeField] public CharacterStat Luck { get; private set; }
    [field: SerializeField] public CharacterStat Charisma { get; private set; }
    [field: SerializeField] public CharacterStat Wisdom { get; private set; }

    public Dictionary<CharacterStatType, CharacterStat> CharacterStats { get; private set; }
    private Dictionary<DerivedCharacterStatType, float> derivedStats = new Dictionary<DerivedCharacterStatType, float>();
    public event Action<DerivedCharacterStatType, float> OnDerivedStatChanged;


    void Awake()
    {
        InitializeStats();

        foreach (KeyValuePair<CharacterStatType, CharacterStat> kvp in CharacterStats)
        {
            kvp.Value.stat.OnBaseValueChanged += ApplyAllEffects;
            kvp.Value.stat.OnValueChanged += ApplyAllEffects;
        }
    }
    private void InitializeStats()
    {
        CharacterStats = new Dictionary<CharacterStatType, CharacterStat>();

        // Add Character Stats to dictionnary
        CharacterStats.Add(CharacterStatType.Constitution, Constitution);
        CharacterStats.Add(CharacterStatType.Endurance, Endurance);
        CharacterStats.Add(CharacterStatType.Strength, Strength);
        CharacterStats.Add(CharacterStatType.Dexterity, Dexterity);
        CharacterStats.Add(CharacterStatType.Intelligence, Intelligence);
        CharacterStats.Add(CharacterStatType.Luck, Luck);
        CharacterStats.Add(CharacterStatType.Charisma, Charisma);
        CharacterStats.Add(CharacterStatType.Wisdom, Wisdom);
    }

    public float GetStatValue(CharacterStatType statType)
    {
        if (CharacterStats.TryGetValue(statType, out CharacterStat characterStat))
        {
            return characterStat.stat.Value;
        }
        else
        {
            return 0;
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
        foreach (KeyValuePair<CharacterStatType, CharacterStat> kvp in CharacterStats)
        {
            foreach (var effect in kvp.Value.derivedStatEffects)
            {
                ApplyEffect(kvp.Value, effect);
            }
        }
    }

    private void ApplyEffect(CharacterStat statInfo, DerivedCharacterStatEffect effect)
    {
        float statValue = statInfo.stat.Value;
        float bonus = statValue * effect.effectPerPoint;
        SetDerivedStatValue(effect.derivedStatType, bonus);
    }
}
