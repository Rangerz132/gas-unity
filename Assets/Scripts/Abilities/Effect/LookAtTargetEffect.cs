using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAtTargetEffect", menuName = "Abilities/Effect/Look At Target", order = 0)]
public class LookAtTargetEffect : EffectStrategy
{
    public override void StartEffect(AbilityData data, Action finished)
    {
        data.User.transform.LookAt(data.targetedPoints);
        finished();
    }
}
