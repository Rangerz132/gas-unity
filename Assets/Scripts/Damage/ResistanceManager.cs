using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResistanceManager : MonoBehaviour
{
    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public float CalculateResistanceModifier(DamageType damageType)
    {
        float resistanceModifier = 0;
        switch (damageType)
        {
            case DamageType.Magic:
                resistanceModifier = CalculateResistance(CharacterStatType.Intelligence, DerivedCharacterStatType.MagicResistance);
                break;
            case DamageType.Physic:
                resistanceModifier = CalculateResistance(CharacterStatType.Strength, DerivedCharacterStatType.PhysicalResistance);
                break;
        }

        return resistanceModifier;
    }

    public float CalculateResistance(CharacterStatType characterStatType, DerivedCharacterStatType derivedCharacterStatType)
    {
        CharacterStat characterStat = characterStatsManager.CharacterStats[characterStatType];

        float statValue = characterStatsManager.GetStatValue(characterStatType);
        float resistanceModierValue = 0;

        for (var i = 0; i < characterStat.derivedStatEffects.Count; i++)
        {
            if (characterStat.derivedStatEffects[i].derivedStatType.Equals(derivedCharacterStatType))
            {
                resistanceModierValue = characterStat.derivedStatEffects[i].effectPerPoint;
            }
        }

        return statValue * resistanceModierValue;
    }
}
