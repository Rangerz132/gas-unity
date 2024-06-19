using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SphereZone", menuName = "Abilities/Zone/Sphere", order = 0)]
public class SphereZone : ZoneStrategy
{
    public override IEnumerable<GameObject> GetGameObjectsInZone(Vector3 point)
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(point, Size / 2, Vector3.up, 0);

        foreach (var hit in raycastHits)
        {
            yield return hit.collider.gameObject;
        }
    }

    public override void DrawGizmos(Vector3 position) {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, Size / 2);
    }
}

