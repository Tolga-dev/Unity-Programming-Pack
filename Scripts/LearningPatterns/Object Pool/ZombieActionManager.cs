using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieActionManager : MonoBehaviour, IEntityPool
{
    public void OnEntitySpawnEvents()
    {
        float vectorx = Random.Range(0, 2);
        float vectory = Random.Range(0, 2);
        float vectorz = Random.Range(0, 2);

        Vector3 velocity = new Vector3(vectorx, vectory, vectorz);
         
        GetComponent<Rigidbody>().velocity = velocity * 10;
    }

}

