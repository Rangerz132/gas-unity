using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HealthPopPoolManager : PoolManager<HealthPopType>
{
    public static Func<HealthPopType, GameObject> OnGetHealthPop { get; private set; }

    private void OnEnable()
    {
        OnGetHealthPop += GetPooledObject;
    }

    private void OnDisable()
    {
        OnGetHealthPop -= GetPooledObject;
    }
}