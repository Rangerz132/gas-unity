using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnProjectileBarragePrefabEffect", menuName = "Abilities/Effect/Spawn Projectile Barrage Prefab", order = 0)]
public class SpawnProjectileBarragePrefabEffect : EffectStrategy
{
    [SerializeField] private Projectile projectilePrefabToSpawn;
    [SerializeField] private int amount;
    [SerializeField] private float spreadOffset = 1f;

    public override void StartEffect(AbilityData data, Action finished)
    {
        Vector3 targetPoint = data.targetedPoints;
        Vector3 directionToTarget = (targetPoint - data.User.transform.position).normalized;

        // Calculate the orthogonal direction to the target direction for spreading projectiles
        Vector3 orthogonalDirection = Vector3.Cross(directionToTarget, Vector3.up).normalized;

        for (int i = 0; i < amount; i++)
        {
            // Calculate the offset for the current projectile
            float offset = (i - (amount - 1) / 2f) * spreadOffset;

            // Calculate the spawn position with the offset
            Vector3 spawnPosition = data.User.transform.position + orthogonalDirection * offset;
       
            // Spawn projectile
            GameObject projectileGameObject = ProjectilePoolManager.OnGetProjectile?.Invoke(projectilePrefabToSpawn.Type);
            Projectile projectileInstance = projectileGameObject.GetComponent<Projectile>();
            projectileInstance.transform.position = spawnPosition;

            // Calculate the new target point with the same offset
            Vector3 newTargetPoint = targetPoint + orthogonalDirection * offset;

            // Update targetedPoints with the new target point
            projectileInstance.SetData(data, newTargetPoint);
        }

        finished();
    }
}
