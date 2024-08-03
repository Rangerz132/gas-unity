using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnProjectileSprayPrefabEffect", menuName = "Abilities/Effect/Spawn Projectile Spray Prefab", order = 0)]
public class SpawnProjectileSprayPrefabEffect : EffectStrategy
{
    [SerializeField] private Projectile projectilePrefabToSpawn;
    [SerializeField] private int amount;
    [SerializeField] private float spreadAngle = 15f;

    public override void StartEffect(AbilityData data, Action finished)
    {
        // Calculate the starting angle and the angle increment
        float startAngle = -spreadAngle / 2f;
        float angleIncrement = spreadAngle / (Mathf.Max(amount - 1, 1));

        Vector3 targetPoint = data.targetedPoints;
        Vector3 directionToTarget = (targetPoint - data.User.transform.position).normalized;

        for (int i = 0; i < amount; i++)
        {
            // Calculate the rotation for the current projectile
            float currentAngle = startAngle + i * angleIncrement;

            // Calculate the rotation quaternion
            Quaternion rotation = Quaternion.AngleAxis(currentAngle, Vector3.up);

            // Spawn projectile
            GameObject projectileGameObject = ProjectilePoolManager.OnGetProjectile?.Invoke(projectilePrefabToSpawn.Type);
            Projectile projectileInstance = projectileGameObject.GetComponent<Projectile>();
            projectileInstance.transform.position = data.User.transform.position;

            // Apply the rotation to the direction to get the new target point
            Vector3 newTargetPoint = data.User.transform.position + (rotation * directionToTarget) * (targetPoint - data.User.transform.position).magnitude;

            // Update targetedPoints with the new target point
            projectileInstance.SetData(data, newTargetPoint);
        }

        finished();
    }
}
