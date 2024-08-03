using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Abilities/Effect/Health/Damage", order = 0)]
public class DamageEffect : EffectStrategy
{
    [Header("Damage Value")]
    [SerializeField] private float damageValue;
    [SerializeField] private bool isPercent;
    public float currentValue;

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
                currentValue = isPercent ? damageValue * health.maxHealth / 100 : damageValue;

                // Do damage
                health.TakeDamage(data.User, currentValue);

                if (healthPop) {

                    GameObject healthPopGameObject = HealthPopPoolManager.OnGetHealthPop?.Invoke(HealthPopType.Damage);
                    HealthPop healthPopInstance = healthPopGameObject.GetComponent<HealthPop>();
                    healthPopInstance.Initialize(currentValue.ToString(), health.gameObject.transform.position + healthOffset);
                }
            }
        }

        finished();
    }
}
