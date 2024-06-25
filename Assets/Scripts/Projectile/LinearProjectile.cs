using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : Projectile
{
    [SerializeField] private float speed;

    void Update()
    {
        Move();
        Aim();
    }

    public override void Move()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    public override void Aim()
    {
        transform.LookAt(targetPosition);
    }
}
