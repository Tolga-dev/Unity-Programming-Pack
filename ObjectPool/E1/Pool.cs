using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.E1
{
    [Serializable]
    public class Pool : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, object> _poolDictionary = new Dictionary<string, object>();

        public void CreatePool<T>(string poolId, int size)
        {
            if (!_poolDictionary.ContainsKey(poolId))
            {
                var newPool = new Pool<T>
                {
                    objects = new T[size],
                    size = size
                };

                _poolDictionary.Add(poolId, newPool);
            }
            else
            {
                Debug.LogWarning("Pool with ID " + poolId + " already exists.");
            }
        }

        public void AddObjectToPool<T>(string poolId, T poolObject, int ind)
        {
            if (_poolDictionary.TryGetValue(poolId, out var value))
            {
                ((Pool<T>)value).objects[ind] = poolObject;
            }
            else
            {
                Debug.LogError("Pool with ID " + poolId + " does not exist.");
            }
        }

        public T GetObjectFromPool<T>(string poolId)
        {
            if (_poolDictionary.TryGetValue(poolId, out var value))
            {
                var currentPool = (Pool<T>)value;
                currentPool.ind++;

                if (currentPool.ind >= currentPool.size)
                    currentPool.ind = 0;

                return currentPool.objects[currentPool.ind];
            }
            else
            {
                return default(T);
            }
        }
    }

    [System.Serializable]
    public class Pool<T>
    {
        public int ind = -1;
        public int size;
        [SerializeField] public T[] objects;
    }
}
