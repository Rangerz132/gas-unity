using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WisdomCooldownAbilityEffect : IStatEffect
{
    private float cooldownPerWisdomPoint;

    public WisdomCooldownAbilityEffect(float cooldownPerWisdomPoint)
    {
        this.cooldownPerWisdomPoint = cooldownPerWisdomPoint;
    }

    public void ApplyEffect(CharacterStats characterStats)
    {
        float wisdomValue = characterStats.GetStatValue(CharacterStatType.Wisdom);
        float cooldownBonus = wisdomValue * cooldownPerWisdomPoint;

        characterStats.SetDerivedStatValue(DerivedCharacterStatType.AbilityCooldown, cooldownBonus);
    }
}