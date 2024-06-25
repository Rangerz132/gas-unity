using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected AbilityData data;
    protected GameObject target;
    protected Vector3 targetPosition;
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject hitPrefabToSpawn;
    [SerializeField] protected EffectStrategy[] collisionEffectStrategies;


    private void Update()
    {
        DieOverlifetime();
    }

    public void SetData(AbilityData data, GameObject target)
    {
        this.data = data;
        this.target = target;
        this.targetPosition = target.transform.position;
    }

    private void DieOverlifetime()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (EffectStrategy collisionEffect in collisionEffectStrategies)
        {
            collisionEffect.StartEffect(data, EffectFinished);
        }

        Instantiate(hitPrefabToSpawn, other.gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public virtual void Move() { }

    public virtual void Aim() { }

    private void EffectFinished() { }
}
