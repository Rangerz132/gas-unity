using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Abilities/Effect/Heal", order = 0)]
public class HealEffect : EffectStrategy
{
    [SerializeField] private float healValue;
    [SerializeField] private bool isPercent;
    private float currentValue;

    public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action finished)
    {
        foreach (var target in targets)
        {
            // Get health component
            if (target.TryGetComponent<Health>(out Health health))
            {
                // Transform current value in percentile if needed
                currentValue = isPercent ? healValue * health.maxHealth / 100 : healValue;

                // Heal
                health.Heal(user, currentValue);
            }
        }
    }
}