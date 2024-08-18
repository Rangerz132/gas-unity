using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public float CalculateDamageModifier(DamageType damageType)
    {
        float damagerModifier = 0;
        switch (damageType)
        {
            case DamageType.Magic:
                damagerModifier = CalculateDamage(CharacterStatType.Intelligence, DerivedCharacterStatType.MagicDamage);
                break;
            case DamageType.Physic:
                damagerModifier = CalculateDamage(CharacterStatType.Strength, DerivedCharacterStatType.PhysicalDamage);
                break;
        }

        return damagerModifier;
    }

    public float CalculateDamage(CharacterStatType characterStatType, DerivedCharacterStatType derivedCharacterStatType)
    {
        CharacterStat characterStat = characterStatsManager.CharacterStats[characterStatType];

        float statValue = characterStatsManager.GetStatValue(characterStatType);
        float damageModierValue = 0;

        for (var i = 0; i < characterStat.derivedStatEffects.Count; i++)
        {
            if (characterStat.derivedStatEffects[i].derivedStatType.Equals(derivedCharacterStatType))
            {
                damageModierValue = characterStat.derivedStatEffects[i].effectPerPoint;
            }
        }

        return statValue * damageModierValue;
    }
}
