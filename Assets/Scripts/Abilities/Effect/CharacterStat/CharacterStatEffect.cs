using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsBuffEffect", menuName = "Abilities/Effect/CharacterStat/Character Stat", order = 0)]
public class CharacterStatBuffEffect : EffectStrategy
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
            if (target.TryGetComponent<CharacterStatsManager>(out CharacterStatsManager characterStatsManager))
            {
                characterStatsManager.CharacterStats[characterStatType].stat.AddModifier(statModifier);    
            }
        }

        finished();
    }
}
