using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ValueType
{
    Max,
    MaxEqual,
    Equal,
    MinEqual,
    Min
}

[CreateAssetMenu(fileName = "HealthFilter", menuName = "Abilities/Filter/Health", order = 0)]
public class HealthFilter : FilterStrategy
{
    [SerializeField] private ValueType valueType = ValueType.Max;
    [SerializeField] private float value;
    [SerializeField] private bool isPercent;
    private float currentValue;

    public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
    {
        foreach (var gameObject in objectsToFilter)
        {
            // Get health component
            if (gameObject.TryGetComponent<HealthManager>(out HealthManager healthManager))
            {
                // Transform current value in percentile if needed
                currentValue = isPercent ? (healthManager.CurrentHealth / healthManager.MaxHealth) * 100 : value;

                switch (valueType)
                {
                    case ValueType.Max:
                        if (currentValue < value)
                        {
                            yield return gameObject;
                        }
                        break;
                    case ValueType.MaxEqual:
                        if (currentValue <= value)
                        {
                            yield return gameObject;
                        }
                        break;
                    case ValueType.Equal:
                        if (currentValue == value)
                        {
                            yield return gameObject;
                        }
                        break;
                    case ValueType.MinEqual:
                        if (currentValue >= value)
                        {
                            yield return gameObject;
                        }
                        break;
                    case ValueType.Min:
                        if (currentValue > value)
                        {
                            yield return gameObject;
                        }
                        break;
                }
            }
        }
    }
}
