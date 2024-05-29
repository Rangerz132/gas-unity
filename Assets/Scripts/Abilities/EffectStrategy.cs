using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EffectStrategy : ScriptableObject
{
    public abstract void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action finished);
}
