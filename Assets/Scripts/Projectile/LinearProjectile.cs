using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : Projectile
{
    [SerializeField] private float speed;

    private Vector3 direction;

    void Start()
    {
        Aim();
    }

    void  Update()
    {
        Move();
    }

    public override void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public override void Aim()
    {
        direction = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
