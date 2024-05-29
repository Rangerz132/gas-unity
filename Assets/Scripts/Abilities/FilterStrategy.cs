using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class FilterStrategy : ScriptableObject
{
    public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter);
}
