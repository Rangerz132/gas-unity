using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackCooldown", menuName = "Abilities/Cooldown/Stack", order = 0)]
public class StackCooldown : CooldownStrategy
{
    [SerializeField] private int stackAmount;
    [SerializeField] private float stackRechargeTime;
    private int currentStack;

    private void OnEnable()
    {
        currentStack = stackAmount;
        IsReady = currentStack > 0;
        IsRecharging = false;
    }

    public override void StartCooldown(AbilityData data)
    {
        if (currentStack > 0)
        {
            currentStack--;
            IsReady = currentStack > 0;
            if (!IsRecharging)
            {
                data.StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        IsRecharging = true;
        while (currentStack < stackAmount)
        {
            remainingTime = stackRechargeTime;
            while (remainingTime > 0)
            {
                yield return null;
                remainingTime -= Time.deltaTime;
            }
            currentStack++;
            IsReady = true;
        }
        IsRecharging = false;
    }
}

