using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
     public static ItemAssets Instance { get; private set; }

     private void Awake()
     {
          Instance = this;
     }

     public GameObject prefabItemWorldController;
   
     public Sprite Gun;
     public GameObject GunPref;
     public Transform GunTransform;
     
     public Sprite Ammo;
     public GameObject AmmoPref;
     
     
     public Sprite Grenade;
     public GameObject GrenadePref;
     public Transform GrenadeTransform;
  



}
