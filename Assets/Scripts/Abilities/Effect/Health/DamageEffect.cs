using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { 
    Physic,
    Magic
}

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Abilities/Effect/Health/Damage", order = 0)]
public class DamageEffect : EffectStrategy
{
    [Header("Damage")]
    [SerializeField] private float damageValue;
    [SerializeField] private bool isPercent;
    [SerializeField] private DamageType damageType;
    private float baseValue;

    [Header("Pop")]
    [SerializeField] private HealthPop healthPop;
    [SerializeField] private Vector3 healthOffset;

    public override void StartEffect(AbilityData data, Action finished)
    {

        DamageManager damageManager = data.User.GetComponent<DamageManager>();

        foreach (var target in data.targets)
        {
            // Get health component
            if (target.TryGetComponent<Health>(out Health health))
            {

                // Transform current value in percentile if needed
                baseValue = isPercent ? damageValue * health.MaxHealth / 100 : damageValue;

                float currentValue = baseValue;

                if (damageManager != null)
                {
                    float damageBonus = damageManager.CalculateDamageModifier(damageType);
                    currentValue += damageBonus;
                }

                // Do damage
                health.TakeDamage(data.User, currentValue);

                if (healthPop)
                {
                    GameObject healthPopGameObject = HealthPopPoolManager.OnGetHealthPop?.Invoke(HealthPopType.Damage);
                    HealthPop healthPopInstance = healthPopGameObject.GetComponent<HealthPop>();
                    healthPopInstance.Initialize(currentValue.ToString(), health.gameObject.transform.position + healthOffset);
                }
            }
        }

        finished();
    }
}
