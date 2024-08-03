using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    private IObjectPool<GameObject> pool;

    public void SetPool(IObjectPool<GameObject> objectPool)
    {
        pool = objectPool;
    }

    public void GetFromPool()
    {
        pool.Get();
    }

    public void ReturnToPool()
    {
        pool.Release(gameObject);
    }
}

