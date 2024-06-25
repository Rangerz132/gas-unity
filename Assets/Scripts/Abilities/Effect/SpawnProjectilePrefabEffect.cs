using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnProjectilePrefabEffect", menuName = "Abilities/Effect/Spawn Projectile Prefab", order = 0)]
public class SpawnProjectilePrefabEffect : EffectStrategy
{
    [SerializeField] private Projectile projectilePrefabToSpawn;

    public override void StartEffect(AbilityData data, Action finished)
    {
        foreach (GameObject target in data.targets)
        {
            Projectile projectileInstance = Instantiate(projectilePrefabToSpawn);
            projectileInstance.transform.position = data.User.transform.position;
            projectileInstance.SetData(data, target);
        }

        finished();
    }
}
