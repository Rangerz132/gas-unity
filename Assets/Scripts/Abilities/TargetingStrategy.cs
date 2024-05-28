using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TargetingStrategy : ScriptableObject
{
    public abstract void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> finished);
}
