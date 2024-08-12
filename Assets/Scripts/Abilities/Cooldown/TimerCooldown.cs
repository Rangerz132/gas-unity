using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimerCooldown", menuName = "Abilities/Cooldown/Timer", order = 0)]
public class TimerCooldown : CooldownStrategy
{
    private void OnEnable()
    {
        remainingTime = 0;
        IsReady = remainingTime <= 0;
        rechargeTime = baseRechargeTime;
    }

    public override void StartCooldown(AbilityData data)
    {
        data.StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        IsReady = false;
        remainingTime = rechargeTime;
        while (remainingTime > 0)
        {
            yield return null;
            remainingTime -= Time.deltaTime;
        }
        IsReady = true;
        remainingTime = 0;
    }
}
