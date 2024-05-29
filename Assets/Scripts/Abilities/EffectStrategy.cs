using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EffectStrategy : ScriptableObject
{
    public abstract void StartEffect(AbilityData data, Action finished);
}
