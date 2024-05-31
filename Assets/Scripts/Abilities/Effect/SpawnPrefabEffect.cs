using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPrefabEffect", menuName = "Abilities/Effect/Spawn Prefab", order = 0)]
public class SpawnPrefabEffect : EffectStrategy
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float destroyDelay = -1;

    public override void StartEffect(AbilityData data, Action finished)
    {
        data.StartCoroutine(Effect(data, finished));
    }

    private IEnumerator Effect(AbilityData data, Action finished)
    {
        GameObject prefabInstance = Instantiate(prefabToSpawn);
        prefabInstance.transform.position = data.targetedPoints;

        if (destroyDelay > 0)
        {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(prefabInstance);
        }
        finished();
    }
}
