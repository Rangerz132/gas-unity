using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : Projectile
{
    [SerializeField] private float speed;


    protected override void Update()
    {
        base.Update();
        Move();
        Aim();
    }

    public override void Move()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public override void Aim()
    {
        transform.LookAt(target.transform.position);
    }
}
