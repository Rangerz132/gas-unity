using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SelfTargeting", menuName = "Abilities/Targeting/Self", order = 0)]
public class SelfTargeting : TargetingStrategy
{
    public override void StartTargeting(AbilityData data, Action finished)
    {
        data.targetedPoints = data.User.transform.position;
        data.targets = new List<GameObject>() {data.User} ;
        finished();
    }
}

