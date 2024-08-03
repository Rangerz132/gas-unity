using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager<TEnum> : MonoBehaviour where TEnum : Enum
{
    [System.Serializable]
    private class PoolInfo
    {
        public TEnum type;
        public GameObject prefab;
        public int defaultPoolSize = 20;
        public int maxPoolSize = 100;
    }

    [SerializeField] private List<PoolInfo> poolInfos;

    private Dictionary<TEnum, ObjectPool<GameObject>> objectPools;

    void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        objectPools = new Dictionary<TEnum, ObjectPool<GameObject>>();
        foreach (var info in poolInfos)
        {
            objectPools[info.type] = new ObjectPool<GameObject>(
                () => CreatePooledObject(info.type),
                OnGetObjectFromPool,
                OnReleaseObjectToPool,
                OnDestroyPooledObject,
                true,
                info.defaultPoolSize,
                info.maxPoolSize
            );

            for (int i = 0; i < info.defaultPoolSize; i++)
            {
                GameObject obj = CreatePooledObject(info.type);
                objectPools[info.type].Release(obj);
            }
        }
    }

    private GameObject CreatePooledObject(TEnum type)
    {
        PoolInfo info = poolInfos.Find(p => p.type.Equals(type));
        GameObject instance = Instantiate(info.prefab);
        instance.transform.SetParent(transform);
        instance.AddComponent<PooledObject>().SetPool(objectPools[info.type]);
        return instance;
    }

    private void OnGetObjectFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReleaseObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetPooledObject(TEnum type)
    {
        if (objectPools.TryGetValue(type, out ObjectPool<GameObject> pool))
        {
            return pool.Get();
        }
        return null;
    }

    public void ReleasePooledObject(TEnum type, GameObject obj)
    {
        if (objectPools.TryGetValue(type, out ObjectPool<GameObject> pool))
        {
            pool.Release(obj);
        }
    }
}