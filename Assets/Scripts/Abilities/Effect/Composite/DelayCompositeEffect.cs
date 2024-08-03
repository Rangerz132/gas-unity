using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DelayCompositeEffect", menuName = "Abilities/Effect/Composite/Delay Composite", order = 0)]
public class DelayCompositeEffect : EffectStrategy
{
    [SerializeField] private float delay = 0;
    [SerializeField] private EffectStrategy[] delayedEffects;

    public override void StartEffect(AbilityData data, Action finished)
    {
        data.StartCoroutine(DelayedEffects(data, finished));
    }

    private IEnumerator DelayedEffects(AbilityData data, Action finished)
    {
        yield return new WaitForSeconds(delay);

        foreach (var effect in delayedEffects)
        {
            effect.StartEffect(data, finished);
        }
    }
}
