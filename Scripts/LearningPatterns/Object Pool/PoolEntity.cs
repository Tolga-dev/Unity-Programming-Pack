using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PoolEntity : MonoBehaviour
{
    [System.Serializable]
    public class PoolClass
    {
        public string tag;
        public GameObject entityPrefab;
        public int entitySize;
    }

    public List<PoolClass> EntityPools;
    public Dictionary<string, Queue<GameObject>> EntityPoolDict;

    #region Singleton

    public static PoolEntity Instance;

    private void Awake()
    {
        Instance = this;
        // throw new NotImplementedException();
    }

    #endregion

    void Start()
    {
        EntityPoolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (PoolClass pool in EntityPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.entitySize; i++)
            {
                GameObject entity = Instantiate(pool.entityPrefab);
                entity.SetActive(false);
                objectPool.Enqueue(entity);
            }
            EntityPoolDict.Add(pool.tag, objectPool);
        }
 
    }
    
    public GameObject SpawnFromEntityPool(String tag, Vector3 position, Quaternion rotation)
    {
        if (!EntityPoolDict.ContainsKey(tag))
        {
            Debug.LogWarning(tag + " does not exits");
            return null;
        }
        
        GameObject tospawn = EntityPoolDict[tag].Dequeue();
        
        tospawn.SetActive(true);
        tospawn.transform.position = position;
        tospawn.transform.rotation = rotation;
        
        IEntityPool pooledObj = tospawn.GetComponent<IEntityPool>();

        pooledObj?.OnEntitySpawnEvents();
        
        EntityPoolDict[tag].Enqueue(tospawn);

        return tospawn;
    }
        

}
