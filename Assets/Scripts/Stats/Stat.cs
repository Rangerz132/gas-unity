using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public float baseValue;
    public float Value { get { return CalculateFinalValue(); } }
    public List<StatModifier> StatModifiers { get; private set; }

    public Stat()
    {
        StatModifiers = new List<StatModifier>();
    }

    public Stat(float baseValue) : this()
    {
        this.baseValue = baseValue;
    }

    public void AddModifier(StatModifier statModifier)
    {
        StatModifiers.Add(statModifier);
        StatModifiers.Sort(CompareModifierOrder);
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        StatModifiers.Remove(statModifier);
        StatModifiers.Sort(CompareModifierOrder);
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
        {
            return -1;
        }
        else if (a.order > b.order)
        {
            return 1;
        }

        return 0;
    }

    public void RemoveAllModifiersFromSource(object source)
    {
        for (int i = StatModifiers.Count - 1; i >= 0; i--)
        {
            if (StatModifiers[i].source == source)
            {
                StatModifiers.RemoveAt(i);
            }
        }
    }

    public float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdditive = 0;

        for (int i = 0; i < StatModifiers.Count; i++)
        {

            StatModifier currentStatModifier = StatModifiers[i];

            if (currentStatModifier.type == StatModifierType.Flat)
            {
                finalValue += StatModifiers[i].value;
            }
            else if (currentStatModifier.type == StatModifierType.PercentMultiplier)
            {
                sumPercentAdditive += StatModifiers[i].value;

                if (i + 1 >= StatModifiers.Count || StatModifiers[i + 1].type != StatModifierType.PercentAdditive)
                {
                    finalValue *= 1 + sumPercentAdditive;
                    sumPercentAdditive = 0;
                }
            }
            else if (currentStatModifier.type == StatModifierType.PercentMultiplier)
            {
                finalValue *= 1 + currentStatModifier.value;
            }
        }

        return (float)Mathf.Round(finalValue);
    }
}
