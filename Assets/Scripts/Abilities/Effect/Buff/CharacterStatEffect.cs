using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsBuffEffect", menuName = "Abilities/Effect/Buff/Character Stats Buff", order = 0)]
public class CharacterStatsBuffEffect : EffectStrategy
{
    [Header("Modifier")]
    public float value;
    public StatModifierType statModifierType;
    public int order;
    public object source;

    [Header("Character Stat")]
    public CharacterStatType characterStatType;

    public override void StartEffect(AbilityData data, Action finished)
    {
        StatModifier statModifier = new StatModifier(value, statModifierType, order, data.User);

        foreach (var target in data.targets)
        {
            // Add buff
            if (target.TryGetComponent<CharacterStats>(out CharacterStats characterStats))
            {
                characterStats.Stats[characterStatType].AddModifier(statModifier);    
            }
        }

        finished();
    }
}
