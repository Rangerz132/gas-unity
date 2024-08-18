using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Abilities/Effect/Health/Heal", order = 0)]
public class HealEffect : EffectStrategy
{
    [Header("Heal")]
    [SerializeField] private float healValue;
    [SerializeField] private bool isPercent;
    private float currentValue;

    [Header("Pop")]
    [SerializeField] private HealthPop healthPop;
    [SerializeField] private Vector3 healthOffset;

    public override void StartEffect(AbilityData data, Action finished)
    {
        foreach (var target in data.targets)
        {
            // Get health component
            if (target.TryGetComponent<Health>(out Health health))
            {
                // Transform current value in percentile if needed
                currentValue = isPercent ? healValue * health.MaxHealth / 100 : healValue;

                // Heal
                health.Heal(data.User, currentValue);

                if (healthPop)
                {
                    GameObject healthPopGameObject = HealthPopPoolManager.OnGetHealthPop?.Invoke(HealthPopType.Heal);
                    HealthPop healthPopInstance = healthPopGameObject.GetComponent<HealthPop>();
                    healthPopInstance.Initialize(currentValue.ToString(), health.gameObject.transform.position + healthOffset);

                
                }
            }
        }

        finished();
    }
}
