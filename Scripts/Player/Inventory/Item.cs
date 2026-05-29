using System;
using UnityEngine;



namespace Player.Inventory
{
    [Serializable]
    public class Item
    {
        public enum ItemTypes
        {
            Grenade,
            Bandage,
            Gun,
            Ammo
        }
    
        public ItemTypes itemTypes;
        public int amount;
        public GameObject itemPref;
        public Transform ItemTransform;
        public int Id;
        public int AmmoCapacity;
        public int CurrentAmmoAmount;
        public float Damage;
        public float Recoil;
        public float Reload;

        public Sprite GetItemSprite()
        {
            switch (itemTypes)
            {   
                default:
                    case ItemTypes.Grenade: return ItemAssets.Instance.Grenade;
                    case ItemTypes.Gun: return ItemAssets.Instance.Gun;
                    case ItemTypes.Ammo: return ItemAssets.Instance.Ammo;
                
            }
        }

        public GameObject GetItemPrefab()
        {
            switch (itemTypes)
            {   
                default:
                case ItemTypes.Grenade: return ItemAssets.Instance.GrenadePref;
                case ItemTypes.Gun: return ItemAssets.Instance.GunPref;
                case ItemTypes.Ammo: return ItemAssets.Instance.AmmoPref;
                
            }
        }
        
        public Transform GetItemTransform()
        {
            switch (itemTypes)
            {   
                default:
                case ItemTypes.Grenade: return ItemAssets.Instance.GrenadeTransform;
                case ItemTypes.Gun: return ItemAssets.Instance.GunTransform;
                
            }
        }
        
        public bool IsStackable()
        {
            switch (itemTypes)
            {
                default:
                    case ItemTypes.Grenade:
                    case ItemTypes.Bandage:
                    case ItemTypes.Ammo:
                        return true;
                    case ItemTypes.Gun: 
                        return false;

            }
        }
    }
}