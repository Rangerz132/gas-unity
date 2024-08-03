using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    Fire,
    Leaf,
    Wind,
    Void,
}

public class Hit : MonoBehaviour
{
    [field: SerializeField] public HitType Type { get; private set; }
    [SerializeField] private float lifetime;
    private float currentLifeTime;

    private void OnEnable()
    {
        currentLifeTime = lifetime;
    }

    private void Update()
    {
        DieOverlifetime();
    }

    private void DieOverlifetime()
    {
        currentLifeTime -= Time.deltaTime;

        if (currentLifeTime <= 0)
        {
            GetComponent<PooledObject>().ReturnToPool();
        }
    }

}
