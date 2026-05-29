using System;
using System.Collections.Generic;
using System.Threading;
using BaseStates;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerSkills.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PrimaryPlayer.PlayerComponentManagers.Inventory
{
    [Serializable]
    public class Item // we will add this to a prefab object. 
    {
        public ItemTypes itemTypes;
        
        [Header("will be gotten from Prefab")]
        public int canBeStackedMax;
        [SerializeField] public Sprite itemSprite;
        [SerializeField] public GameObject itemPref;
        [SerializeField] public Transform itemOnPlayerTransform;
        [SerializeField] public TextMeshPro itemDisPlayer;

        
        public bool IsStackable()
        {
            return canBeStackedMax != 1;
        }
        public Sprite GetItemSprite()
        {
            return itemSprite;
        }

        public GameObject GetItemPrefab()
        {
            return itemPref;
        }

    }

    #region Enums
        
        [Serializable]
        public enum ItemTypes
        {
            Gun,
            Ammo,
            Bandage,
            Drink,
            Grenade,
        } 
        [Serializable]
        public enum GunTypes
        {
            None,
            LaserGun,
            ShotGun,
            Rifle,
            SingleHandGun,
            GravityGun,
            IntelligentTargetingSystemGun
        }
        [Serializable]
        public enum BandageTypes
        {
            None,
            SmallBandage,
            MediumBandage,
            BigBandage,
            AlienBandage,
        }
        [Serializable]
        public enum GrenadeTypes
        {
            None,
            SimpleGrenade,
            MediumGrenade,
            BigGrenade,
            AlienGrenade,
            GodGrenade
        }
        [Serializable]
        public enum DrinkTypes
        {
            None,
            SmallDrink,
            BigDrink,
             
        }
        [Serializable]
        public enum ItemExtremer
        {
            None,
            GlowMaker,
            ExtremeMaker
        }
    #endregion
    
    #region General Class Region To Use In The Future
    public abstract class ItemController
    {
        public abstract GameObject GetItemPrefab();
        public abstract void GetItemType();
        public abstract Item GetItem();
        
    }
 
    [Serializable]
    public class Gun : ItemController
    {
        public Item Item = new Item();
        public float Damage;
        public float MaxAmmoCanHold;
        public float Recoil; //  time between shoot
        public float Reload;
        public float ReloadTime;
        public float CurrentAmmoAmount;
        [SerializeField] public GunTypes SelectedGunType;
        [SerializeField] public ItemExtremer SelectedGunExtremer = ItemExtremer.None;
        [SerializeField] public ItemTypes ItemType = ItemTypes.Gun;
        public override GameObject GetItemPrefab()
        {
            return Item.GetItemPrefab();
        }

        public override void GetItemType()
        {
            Debug.Log(SelectedGunType);
        }

        public override Item GetItem()
        {
            return Item;
        }
        
    }
    
    [Serializable]
    public class Bandage : ItemController
    {
        public Item Item = new Item();
        public float RecoveryTime;
        public float RecoveryPower;
        [SerializeField] public ItemTypes ItemType;
        [SerializeField] public ItemExtremer SelectedBandageExtremer;
        [SerializeField] public BandageTypes BandageType;
        public override GameObject GetItemPrefab()
        {
            return Item.GetItemPrefab();
        }
        public override void GetItemType()
        {
            Debug.Log(BandageType);
        }
        public override Item GetItem()
        {
            return Item;
        }
    }
    
    [Serializable]
    public class Ammo : ItemController
    {
        public Item Item = new Item();
        [SerializeField] public ItemTypes ItemType;
        [SerializeField] public ItemExtremer SelectedAmmoExtremer;
        public GunTypes GunTypes;
        public override GameObject GetItemPrefab()
        {
            return Item.GetItemPrefab();
        }
        public override void GetItemType()
        {
            Debug.Log(GunTypes);
        }
        public override Item GetItem()
        {
            return Item;
        }
    }
    
    [Serializable]
    public class Grenade : ItemController
    {
        public Item Item = new Item();

        public float ExplosionPower;
        public float TargetRange;
        public float ExplosionTime;
        [SerializeField] public ItemTypes ItemType;
        [SerializeField] public ItemExtremer SelectedGrenadeExtremer;
        [SerializeField] public GrenadeTypes GrenadeTypes;
        public override GameObject GetItemPrefab()
        {
            return Item.GetItemPrefab();
        }
        public override void GetItemType()
        {
            Debug.Log(GrenadeTypes);
        }
        public override Item GetItem()
        {
            return Item;
        }
        
    }
    [Serializable]
    public class Drink : ItemController
    {
        public Item Item = new Item();
        [SerializeField] public ItemTypes ItemType;
        [SerializeField] public ItemExtremer SelectedGrenadeExtremer;
        [SerializeField] public DrinkTypes DrinkTypes;

        public override GameObject GetItemPrefab()
        {
            return Item.GetItemPrefab();
        }
        public override void GetItemType()
        {
//            Debug.Log(DrinkTypes);
        }
        public override Item GetItem()
        {
            return Item;
        }
    }
  
    #endregion
    
    
    
}


 