using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public enum HealthPopType
{
    Heal,
    Damage,
}

public class HealthPopPoolManager : MonoBehaviour
{
    [System.Serializable]
    private class HealthPopInfo
    {
        public HealthPopType type;
        public GameObject prefab;
    }

    [SerializeField] private List<HealthPopInfo> healthPopInfos;
    [SerializeField] private int defaultPoolSize = 20;
    [SerializeField] private int maxPoolSize = 100;

    private Dictionary<HealthPopType, ObjectPool<GameObject>> healthPopPools;
    public static Func<HealthPopType, GameObject> OnGetHealthPop { get; private set; }

    private void OnEnable()
    {
        OnGetHealthPop += GetHealthPop;
    }

    private void OnDisable()
    {
        OnGetHealthPop -= GetHealthPop;
    }

    void Start()
    {
        healthPopPools = new Dictionary<HealthPopType, ObjectPool<GameObject>>();

        foreach (var info in healthPopInfos)
        {
            healthPopPools[info.type] = new ObjectPool<GameObject>(
                 () => CreateHealthPops(info.type),
                 OnGetHealthPopFromPool,
                 OnReleaseHealthPopToPool,
                 OnDestroyHealthPop,
                 true,
                 defaultPoolSize,
                 maxPoolSize
             );

            for (int i = 0; i < defaultPoolSize; i++)
            {
                GameObject healthPop = CreateHealthPops(info.type);
                healthPopPools[info.type].Release(healthPop);
            }
        }
    }

    private GameObject CreateHealthPops(HealthPopType type)
    {

        HealthPopInfo info = healthPopInfos.Find(p => p.type == type);
        GameObject healthPopInstance = Instantiate(info.prefab);
        healthPopInstance.gameObject.transform.SetParent(gameObject.transform);

        healthPopInstance.gameObject.AddComponent<PooledHealtPop>().SetPool(healthPopPools[info.type]);
        return healthPopInstance;
    }

    private void OnGetHealthPopFromPool(GameObject healthPop)
    {
        healthPop.SetActive(true);
    }

    private void OnReleaseHealthPopToPool(GameObject healthPop)
    {
        healthPop.SetActive(false);
    }

    private void OnDestroyHealthPop(GameObject healthPop)
    {
        Destroy(healthPop);
    }

    private GameObject GetHealthPop(HealthPopType type)
    {
        if (healthPopPools.TryGetValue(type, out ObjectPool<GameObject> pool))
        {
            return pool.Get();
        }
       
        return null;
    }
}

public class PooledHealtPop : MonoBehaviour
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

