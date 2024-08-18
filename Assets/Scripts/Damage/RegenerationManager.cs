using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationManager : MonoBehaviour
{
    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public float CalculateRegeneration()
    {
        CharacterStat characterStat = characterStatsManager.CharacterStats[CharacterStatType.Intelligence];

        float statValue = characterStatsManager.GetStatValue(CharacterStatType.Intelligence);
        float regenerationModierValue = 0;

        for (var i = 0; i < characterStat.derivedStatEffects.Count; i++)
        {
            if (characterStat.derivedStatEffects[i].derivedStatType.Equals(DerivedCharacterStatType.Regeneration))
            {
                regenerationModierValue = characterStat.derivedStatEffects[i].effectPerPoint;
            }
        }

        return statValue * regenerationModierValue;
    }
}
