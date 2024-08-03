using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Fireball,
    LeafGrenade,
    Shuriken,
    VoidStrike,
}

public abstract class Projectile : MonoBehaviour
{
    [field: SerializeField] public ProjectileType Type { get; private set; }

    protected AbilityData data;
    protected GameObject target;
    protected Vector3 targetPosition;
    [SerializeField] private float lifetime;
    private float currentLifeTime;
    [SerializeField] private Hit hitPrefabToSpawn;
    [SerializeField] private bool spawnHitOnDie;
    [SerializeField] protected EffectStrategy[] collisionEffectStrategies;

    private void OnEnable()
    {
        currentLifeTime = lifetime;
    }

    protected virtual void Update()
    {
        DieOverlifetime();
    }

    public void SetData(AbilityData data, GameObject target)
    {
        this.data = data;
        this.target = target;
        this.targetPosition = target.transform.position;
    }

    public void SetData(AbilityData data, Vector3 targetPosition)
    {
        this.data = data;
        this.targetPosition = targetPosition;
    }

    private void DieOverlifetime()
    {
        currentLifeTime -= Time.deltaTime;

        if (currentLifeTime <= 0)
        {
            if (spawnHitOnDie)
            {
                SpawnHit(transform.position);
            }

            GetComponent<PooledObject>().ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 closestPoint = other.ClosestPoint(transform.position);

        List<GameObject> targets = new List<GameObject>();
        targets.Add(other.gameObject);
        data.SetTargets(targets);

        foreach (EffectStrategy collisionEffect in collisionEffectStrategies)
        {
            collisionEffect.StartEffect(data, EffectFinished);
        }

        SpawnHit(closestPoint);


        GetComponent<PooledObject>().ReturnToPool();
    }

    private void SpawnHit(Vector3 position)
    {
        GameObject hitGameObject = HitPoolManager.OnGetHit?.Invoke(hitPrefabToSpawn.Type);
        Hit hitInstance = hitGameObject.GetComponent<Hit>();
        hitInstance.transform.position = position;
    }

    public virtual void Move() { }

    public virtual void Aim() { }

    private void EffectFinished() { }
}
