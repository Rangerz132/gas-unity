using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Demo Targeting", menuName = "Abilities/Targeting/Demo", order = 0)]
public class DemoTargeting : TargetingStrategy
{
    public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> finished)
    {
        finished(null);
    }
}
