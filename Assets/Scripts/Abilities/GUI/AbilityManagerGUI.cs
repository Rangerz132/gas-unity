using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManagerGUI : MonoBehaviour
{
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private AbilitySlotGUI[] abilitySlots;

    private void OnEnable()
    {
        abilityManager.OnAbilityUsed += HandleAbilityUsed;
    }

    private void OnDisable()
    {
        abilityManager.OnAbilityUsed -= HandleAbilityUsed;
    }

    void Start()
    {
        SetAbilitySlots();
    }

    void Update()
    {
        UpdateAbilityCooldown();
    }

    /// <summary>
    /// Override all current ability icon with the one from the ability manager
    /// </summary>
    private void SetAbilitySlots()
    {
        for (var i = 0; i < abilityManager.Abilities.Length; i++)
        {
            abilitySlots[i].ability = abilityManager.Abilities[i];
            abilitySlots[i].SetAbilityIcon(abilityManager.Abilities[i].icon);
            abilitySlots[i].SetAbilityManaCost(abilityManager.Abilities[i].manaCost);          
        }
    }

    /// <summary>
    /// Set the ability cooldown value to the maximum
    /// </summary>
    private void HandleAbilityUsed(Ability ability)
    {
        for (var i = 0; i < abilitySlots.Length; i++)
        {
            if (abilitySlots[i].ability == ability)
            {
                // Set the overlay cooldown remaining time
                abilitySlots[i].abilityCooldownTime = abilityManager.AbilityCooldowns[abilitySlots[i].ability];

                // Initialize the fill amount to the max
                abilitySlots[i].SetAbilityCooldownOverlay(1);
            }
        }
    }

    private void UpdateAbilityCooldown()
    {
        foreach (KeyValuePair<Ability, float> abilityCooldown in abilityManager.AbilityCooldowns)
        {
            for (var i = 0; i < abilitySlots.Length; i++)
            {
                if (abilityCooldown.Key.Equals(abilitySlots[i].ability))
                {
                    var cooldownPercentileValue = abilityCooldown.Value / abilitySlots[i].abilityCooldownTime;
                    abilitySlots[i].SetAbilityCooldownOverlay(cooldownPercentileValue);
                }
            }
        }
    }
}
