using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CooldownStrategy : ScriptableObject
{
    public bool IsReady { get; protected set; } = true;
    public abstract void StartCooldown(AbilityData data);
}
