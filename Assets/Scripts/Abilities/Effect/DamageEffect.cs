using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Abilities/Effect/Damage", order = 0)]
public class DamageEffect : EffectStrategy
{
    [SerializeField] private float damageValue;
    [SerializeField] private bool isPercent;
    private float currentValue;

    public override void StartEffect(AbilityData data, Action finished)
    {
        foreach (var target in data.targets)
        {
            // Get health component
            if (target.TryGetComponent<Health>(out Health health))
            {
                // Transform current value in percentile if needed
                currentValue = isPercent ? damageValue * health.maxHealth / 100 : damageValue;

                // Do damage
                health.TakeDamage(data.User, currentValue);
            }
        }

        finished();
    }
}
