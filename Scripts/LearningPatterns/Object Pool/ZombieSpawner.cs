using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private PoolEntity PoolEntity;

    private void Start()
    {
        PoolEntity = PoolEntity.Instance;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
            foreach (PoolEntity.PoolClass pool in PoolEntity.EntityPools)
                PoolEntity.SpawnFromEntityPool(pool.tag, transform.position, Quaternion.identity);

        //Instantiate(ZombiePrefab, transform.position, Quaternion.identity);
        //throw new NotImplementedException();
    }
    

}
