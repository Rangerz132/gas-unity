using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimerCooldown", menuName = "Abilities/Cooldown/Timer", order = 0)]
public class TimerCooldown : CooldownStrategy
{
    [SerializeField] private float timeValue;
    private float currentTimeValue;

    public override void StartCooldown(AbilityData data)
    {
        data.StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        IsReady = false;
        currentTimeValue = timeValue;
        while (currentTimeValue > 0)
        {
            yield return null;
            currentTimeValue -= Time.deltaTime;
        }
        IsReady = true;
        currentTimeValue = 0;
    }
}
