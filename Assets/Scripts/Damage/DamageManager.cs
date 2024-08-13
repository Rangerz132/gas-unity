using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private CharacterStats characterStats;

    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    public float CalculateDamageModifier()
    {
        float intelligenceValue = characterStats.GetStatValue(CharacterStatType.Intelligence);
        float damageModifier = 1f +(intelligenceValue * 0.005f);
        return damageModifier;
    }
}
