using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Ability", order = 0)]
public class Ability : ScriptableObject
{
    public string id;
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;

    [SerializeField] private TargetingStrategy targetingStrategy;
    [field: SerializeField] public CooldownStrategy CooldownStrategy { get; private set; }
    [SerializeField] private FilterStrategy[] filterStrategies;
    [SerializeField] private EffectStrategy[] effectStrategies;

    /// <summary>
    /// Trigger the ability
    /// </summary>
    /// <param name="user"></param>
    public void Use(GameObject user)
    {
        if (CooldownStrategy == null || CooldownStrategy.IsReady)
        {
            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data, () => TargetAcquired(data));
        }
    }

    /// <summary>
    /// Do something when targets have been acquired
    /// </summary>
    /// <param name="targets"></param>
    private void TargetAcquired(AbilityData data)
    {
        // Apply filter to all gameObjects
        foreach (var filter in filterStrategies)
        {
            data.targets = filter.Filter(data.targets);
        }

        foreach (var effect in effectStrategies)
        {
            effect.StartEffect(data, EffectFinished);
        }

        // Apply cooldown
        CooldownStrategy.StartCooldown(data);
        data.User.GetComponent<AbilityManager>().StartCooldown(this, CooldownStrategy.remainingTime);
    }

    private void EffectFinished() { }
}
