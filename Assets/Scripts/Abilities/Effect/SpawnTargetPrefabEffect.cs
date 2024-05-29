using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTargetPrefabEffect", menuName = "Abilities/Effect/Spawn Target Prefab", order = 0)]
public class SpawnTargetPrefabEffect : EffectStrategy
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float destroyDelay = -1;
    private List<GameObject> prefabs = new List<GameObject>();

    public override void StartEffect(AbilityData data, Action finished)
    {
        data.StartCoroutine(Effect(data, finished));
    }

    private IEnumerator Effect(AbilityData data, Action finished)
    {
        yield return new WaitForSeconds(spawnDelay);

        foreach (var target in data.targets)
        {
            // Get target position
            var targetPosition = target.transform.position;

            // Spawn prefab at target position
            prefabToSpawn = Instantiate(prefabToSpawn);
            prefabToSpawn.transform.position = targetPosition;

            // Add prefabs to the list
            prefabs.Add(prefabToSpawn);
        }

        if (destroyDelay > 0)
        {
            yield return new WaitForSeconds(destroyDelay);

            // Destroy all prefabs in the list
            for (var i = 0; i < prefabs.Count; i++)
            {
                Destroy(prefabs[i]);
            }

            // Clear the list
            prefabs.Clear();
        }
        finished();
    }
}
