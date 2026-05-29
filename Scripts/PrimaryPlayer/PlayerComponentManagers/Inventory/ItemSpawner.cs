using System;
using System.Collections;
using System.Collections.Generic;
using Items.Guns;
using UnityEngine;
using PrimaryPlayer.PlayerSkills.Movement;
using PrimaryPlayer.ScriptableObjects;
using TMPro;
using UnityEngine;


namespace PrimaryPlayer.PlayerComponentManagers.Inventory
{
    public class ItemSpawner : MonoBehaviour
    {

        public ItemSo _itemSo;
        
        [Header("Changeable")]
        public int amount;
        
        public ItemTypes itemTypes;

        public GunTypes gunTypes = GunTypes.None;
        public BandageTypes bandageTypes = BandageTypes.None;
        public GrenadeTypes grenadeTypes = GrenadeTypes.None;
        public DrinkTypes drinkTypes = DrinkTypes.None;
        public ItemExtremer itemExtremer = ItemExtremer.None;
        
        
        private void Awake()
        {
            switch (itemTypes)
            {
                case ItemTypes.Ammo:
                    CreateItem(ref _itemSo.Ammos, ammo => ammo.GunTypes == gunTypes);
                    break;
                case ItemTypes.Bandage:
                    CreateItem(ref _itemSo.Bandages, bandage => bandage.BandageType == bandageTypes);
                    break;
                case ItemTypes.Gun:
                    CreateItem(ref _itemSo.Guns, gun => gun.SelectedGunType == gunTypes);
                    break;
                case ItemTypes.Grenade:
                    CreateItem(ref _itemSo.Grenades, grenade => grenade.GrenadeTypes == grenadeTypes);
                    break;
                case ItemTypes.Drink:
                    CreateItem(ref _itemSo.Drinks, drink => drink.DrinkTypes == drinkTypes);
                    break;
                default:
                    break;
            }
            
            Destroy(gameObject);
        }
        

        private void CreateItem<T>(ref List<T> itemList, Func<T, bool> condition) where T : ItemController
        {
            foreach (var item in itemList)
            {
                if (condition(item))
                {
                    SpawnItemsController(item);
                    break;
                }
            }
        }

        
        private void SpawnItemsController(ItemController item)
        {

            var instantiate = Instantiate(item.GetItemPrefab(), transform.position, Quaternion.identity).
                AddComponent<ItemWorldController>();
            
            var component = instantiate.GetComponent<ItemWorldController>();
            
            var textObject = instantiate.transform.GetChild(0).GetChild(0);
            var text = textObject.GetComponent<TMP_Text>();
            
        
            text.text = amount.ToString();
            component.amount = amount;
            component.ItemController = item;
            component.itemTextObject = text;
            component.ItemController.GetItemType();
            component.dummyItem = item.GetItem();
            //   Debug.Log(item.GetType());

        }

        
        
    }
    
    

    
}