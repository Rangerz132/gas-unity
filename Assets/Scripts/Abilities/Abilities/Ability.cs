using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Ability", order = 0)]
public class Ability : ScriptableObject
{
    public string id;
    public string displayName;
    public string description;
    public Sprite icon;

    [SerializeField] private TargetingStrategy targetingStrategy;

    /// <summary>
    /// Trigger the ability
    /// </summary>
    /// <param name="user"></param>
    public void Use(GameObject user)
    {
        targetingStrategy.StartTargeting(user, TargetAcquired);
    }

    /// <summary>
    /// Do something when targets have been acquired
    /// </summary>
    /// <param name="targets"></param>
    private void TargetAcquired(IEnumerable<GameObject> targets)
    {
        foreach (var target in targets) {
            Debug.Log(target);
        }
    }
}
