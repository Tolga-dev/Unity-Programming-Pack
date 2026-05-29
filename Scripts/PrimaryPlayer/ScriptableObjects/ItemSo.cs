using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BaseStates;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Inventory;
using PrimaryPlayer.PlayerSkills.Attack;
using UnityEngine;

namespace PrimaryPlayer.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemSo", menuName = "SO/ItemSo", order = 0)]
    [Serializable]
    public class ItemSo : ScriptableObject
    {
        
        public List<Gun> Guns = new List<Gun>();
        public List<Bandage> Bandages = new List<Bandage>();
        public List<Grenade> Grenades = new List<Grenade>();
        public List<Ammo> Ammos = new List<Ammo>();
        public List<Drink> Drinks = new List<Drink>();
        
    }


}