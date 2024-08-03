using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitPoolManager : PoolManager<HitType>
{
    public static Func<HitType, GameObject> OnGetHit { get; private set; }

    private void OnEnable()
    {
        OnGetHit += GetPooledObject;
    }

    private void OnDisable()
    {
        OnGetHit -= GetPooledObject;
    }
}
