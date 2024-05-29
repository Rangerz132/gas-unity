using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TargetingStrategy : ScriptableObject
{
    public abstract void StartTargeting(AbilityData data, Action finished);
}
