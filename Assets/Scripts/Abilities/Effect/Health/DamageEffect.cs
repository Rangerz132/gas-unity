using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
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
            if (target.TryGetComponent<HealthManager>(out HealthManager healthManager))
            {

                // Transform current value in percentile if needed
                baseValue = isPercent ? damageValue * healthManager.MaxAttribute / 100 : damageValue;

                float currentValue = baseValue;

                // Calculate damage bonus
                if (damageManager != null)
                {
                    float damageBonus = damageManager.CalculateDamageModifier(damageType);
                    currentValue += damageBonus;
                }

                ResistanceManager resistanceManager = target.GetComponent<ResistanceManager>();

                // Calculate resistance bonus
                if (resistanceManager != null)
                {
                    float resistanceBonus = resistanceManager.CalculateResistanceModifier(damageType);
                    currentValue -= resistanceBonus;
                }

                // Do damage
                healthManager.TakeDamage(data.User, currentValue);

                if (healthPop)
                {
                    GameObject healthPopGameObject = HealthPopPoolManager.OnGetHealthPop?.Invoke(HealthPopType.Damage);
                    HealthPop healthPopInstance = healthPopGameObject.GetComponent<HealthPop>();
                    healthPopInstance.Initialize(currentValue.ToString(), healthManager.gameObject.transform.position + healthOffset);
                }
            }
        }

        finished();
    }
}
