using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ProjectilePoolManager : PoolManager<ProjectileType>
{
    public static Func<ProjectileType, GameObject> OnGetProjectile { get; private set; }

    private void OnEnable()
    {
        OnGetProjectile += GetPooledObject;
    }

    private void OnDisable()
    {
        OnGetProjectile -= GetPooledObject;
    }
}