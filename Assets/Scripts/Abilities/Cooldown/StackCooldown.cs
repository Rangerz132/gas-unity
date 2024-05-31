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
    private float remainingRechargeTime;
    private bool isRecharging;

    private void OnEnable()
    {
        currentStack = stackAmount;
        IsReady = currentStack > 0;
        isRecharging = false;
    }

    public override void StartCooldown(AbilityData data)
    {
        if (currentStack > 0)
        {
            currentStack--;
            IsReady = currentStack > 0;
            if (!isRecharging)
            {
                data.StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        isRecharging = true;
        while (currentStack < stackAmount)
        {
            remainingRechargeTime = stackRechargeTime;
            while (remainingRechargeTime > 0)
            {
                yield return null;
                remainingRechargeTime -= Time.deltaTime;
            }
            currentStack++;
            IsReady = true;
        }
        isRecharging = false;
    }
}

