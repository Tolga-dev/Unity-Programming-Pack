using System;
using System.Collections;
using System.Collections.Generic;
using Player.Inventory;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
     public Item item;
    
     private void Awake()
     {
 
               ItemsWorldController.SpawnItemsWorldController(transform.position, item);

               Destroy(gameObject);
     }
     
}
