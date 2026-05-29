using System;
using System.Collections.Generic;
using Chest.ChestManagers;
using PrimaryPlayer.PlayerComponentManagers.Inventory;
using UnityEngine;

namespace Chest.ChestTypes
{
    
    // start game
    // adding item to chest
    // removing item from chest
    // saving item to json
    // calling back item from json
    
    // if item is empty
        // pass 
    // else 
        // item spawner()
        
    // item spawner()
        // for each
            // iwc
            // create ui()
    
    // create ui()
        // create sprite
        
    public class SmallChest : ChestController
    {
        private const int Size = 5;
  
         
        
        private void Awake()
        {
            itemControllers =  new List<ItemWorldController>(Size) { };
            
        }
        

        private void ItemSpawner()
        {
            
        }
        
 
        
    }
}
 