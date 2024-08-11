using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstitutionHealthEffect : IStatEffect
{
    private float healthPerConstitutionPoint;

    public ConstitutionHealthEffect(float healthPerConstitutionPoint)
    {
        this.healthPerConstitutionPoint = healthPerConstitutionPoint;
    }

    public void ApplyEffect(CharacterStats characterStats)
    {
        float constitutionValue = characterStats.GetStatValue(CharacterStatType.Constitution);
        float healthBonus = constitutionValue * healthPerConstitutionPoint;

        characterStats.SetDerivedStatValue(DerivedCharacterStatType.MaxHealth, healthBonus);
    }
}