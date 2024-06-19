using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BoxZone", menuName = "Abilities/Zone/Box", order = 0)]
public class BoxZone : ZoneStrategy
{
    public override IEnumerable<GameObject> GetGameObjectsInZone(Vector3 point)
    {
        RaycastHit[] raycastHits = Physics.BoxCastAll(point, new Vector3(Size, Size, Size ), Vector3.up);

        foreach (var hit in raycastHits)
        {
            yield return hit.collider.gameObject;
        }
    }

    public override void DrawGizmos(Vector3 position) {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(position, new Vector3(Size, Size, Size));
    }
}

