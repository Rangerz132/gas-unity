using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnProjectilePrefabEffect", menuName = "Abilities/Effect/Spawn Projectile Prefab", order = 0)]
public class SpawnProjectilePrefabEffect : EffectStrategy
{
    [SerializeField] private Projectile projectilePrefabToSpawn;
    [SerializeField] private bool useTargetPoint;

    public override void StartEffect(AbilityData data, Action finished)
    {
        if (useTargetPoint)
        {
            SpawnProjectileForTargetPoint(data);
        }
        else
        {
            SpawnProjectileForTargets(data);
        }

        finished();
    }

    private void SpawnProjectileForTargetPoint(AbilityData data)
    {
        GameObject projectileGameObject = ProjectilePoolManager.OnGetProjectile?.Invoke(projectilePrefabToSpawn.Type);
        
        Projectile projectileInstance = projectileGameObject.GetComponent<Projectile>();
        projectileInstance.transform.position = data.User.transform.position;
        projectileInstance.SetData(data, data.targetedPoints);
        projectileInstance.Aim();
    }

    private void SpawnProjectileForTargets(AbilityData data)
    {
        foreach (GameObject target in data.targets)
        {
            GameObject projectileGameObject = ProjectilePoolManager.OnGetProjectile?.Invoke(projectilePrefabToSpawn.Type);
            Projectile projectileInstance = projectileGameObject.GetComponent<Projectile>();
            projectileInstance.transform.position = data.User.transform.position;
            projectileInstance.SetData(data, target);
            projectileInstance.Aim();
        }
    }
}
