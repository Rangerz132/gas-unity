using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [field: SerializeField] public Ability[] Abilities { get; private set; }

    [field: SerializeField] public Dictionary<Ability, float> AbilityCooldowns = new Dictionary<Ability, float>();

    public event Action<Ability> OnAbilityUsed;

    private CharacterStatsManager characterStatsManager;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    private void OnEnable()
    {
        characterStatsManager.OnDerivedStatChanged += HandleDerivedStatChanged;
    }

    private void OnDisable()
    {
        characterStatsManager.OnDerivedStatChanged -= HandleDerivedStatChanged;
    }

    public void Update()
    {
        UpdateCooldowns();
    }

    private void HandleDerivedStatChanged(DerivedCharacterStatType statType, float newValue)
    {
        if (statType == DerivedCharacterStatType.AbilityCooldown)
        {
            UpdateAbilityCooldown();
        }
    }

    /// <summary>
    /// Set abilities cooldown based on character stats and base value
    /// </summary>
    private void UpdateAbilityCooldown()
    {
        float bonusCooldown = characterStatsManager.GetDerivedStatValue(DerivedCharacterStatType.AbilityCooldown);

        foreach (Ability ability in Abilities)
        {
            ability.CooldownStrategy.rechargeTime = ability.CooldownStrategy.baseRechargeTime - bonusCooldown;

            if (ability.CooldownStrategy.rechargeTime <= 0)
            {
                ability.CooldownStrategy.rechargeTime = 0;
            }
        }
    }


    /// <summary>
    /// Add the Ability to the list of AbilityCooldowns and assign the respective remaining time
    /// </summary>
    /// <param name="ability"></param>
    /// <param name="time"></param>
    public void StartCooldown(Ability ability, float time)
    {
        if (!AbilityCooldowns.ContainsKey(ability))
        {
            AbilityCooldowns.Add(ability, time);
            OnAbilityUsed?.Invoke(ability);
        }
    }

    /// <summary>
    /// Set the cooldown value for each Ability that have been used in the past and 
    /// remove this Ability once the cooldown is done
    /// </summary>
    public void UpdateCooldowns()
    {
        List<Ability> keys = new List<Ability>(AbilityCooldowns.Keys);

        foreach (Ability key in keys)
        {
            foreach (Ability ability in Abilities)
            {
                if (key.Equals(ability))
                {
                    // Update the ability cooldown value to be equal to the same one
                    AbilityCooldowns[key] = ability.CooldownStrategy.remainingTime;

                    // Remove ability cooldown if the cooldown have been reach
                    if (ability.CooldownStrategy.remainingTime <= 0)
                    {
                        AbilityCooldowns.Remove(key);
                    }
                }
            }
        }
    }
}
