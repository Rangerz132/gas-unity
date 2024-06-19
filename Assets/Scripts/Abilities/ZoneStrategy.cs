using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ZoneStrategy : ScriptableObject
{
    [field: SerializeField] public float Size { get; protected set; }
    public abstract IEnumerable<GameObject> GetGameObjectsInZone(Vector3 point);
    public abstract void DrawGizmos(Vector3 position);
}
