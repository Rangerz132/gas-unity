using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CooldownStrategy : ScriptableObject
{
    public bool IsReady { get; protected set; } = true;
    public bool IsRecharging { get; protected set; } = false;

    public float remainingTime;
    public abstract void StartCooldown(AbilityData data);
}
