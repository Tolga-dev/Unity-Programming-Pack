using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

namespace Player.Inventory
{
    public class ClassEventArgs : EventArgs { }

    public interface Interface
    {
        event EventHandler<ClassEventArgs> OnItemListChanged;
    }

    public class Inventory : Interface
    {
        public event EventHandler<ClassEventArgs> OnItemListChanged;
        
        public List<Item> itemList;
        public int IdAssigner = 0;

        private Action<Item> useItemAction;
        public Inventory(Action<Item> useItemAction)
        {
            this.useItemAction = useItemAction;
            itemList = new List<Item>();
            
        }

        public void AddItem(Item item)
        { 
            
            if (item.IsStackable())
            {
                bool itemAlreadyInInventory = false;
                foreach (Item itemInventory in itemList)
                {
                    if (itemInventory.itemTypes == item.itemTypes)
                    {
                        itemInventory.amount += item.amount;
                        Debug.Log(itemInventory.amount);
                        itemAlreadyInInventory = true;
                    }
                }

                if (!itemAlreadyInInventory)
                {
                    itemList.Add(item);
                    if (item.Id == 0)
                    {
                        ++IdAssigner;
                        item.Id = IdAssigner;
                    }
                    //item.itemPref.GetComponent<ItemsWorldController>().item = item; 
                }

            }
            else
            {
                if (item.Id == 0)
                {
                    ++IdAssigner;
                    item.Id = IdAssigner;
                } 
                itemList.Add(item); 
                //item.itemPref.GetComponent<ItemsWorldController>().item = item; 
            }
            OnItemListChanged?.Invoke(this, new ClassEventArgs());
        }
        

        public void RemoveItem(Item item)
        {
            if (item.IsStackable())
            {
                Debug.Log(itemList.Count + " yaes");
                
                Item itemInInventory = null;
                foreach (Item itemInventory in itemList)
                {
                    if (itemInventory.Id == item.Id)
                    {
                        ///////
                       // itemInventory.amount = itemInventory.amount - item.amount;
                       Debug.Log(itemList.Count + " yeasss");
                        itemInInventory = itemInventory;

                        if (itemInInventory != null)
                        {
                            itemList.Remove(itemInInventory);
                            
                        }
                        break;
                    }
                }
                
            }
            else
            {
                Debug.Log(itemList.Count + " yaes");
                Item itemInInventory = null;
                foreach (Item itemInventory in itemList)
                {
                    
                    if (itemInventory.Id == item.Id)
                    {
                        Debug.Log(itemList.Count + " yeasss");
                        itemInInventory = itemInventory;
                        
                        if (itemInInventory != null)
                        {
                            itemList.Remove(itemInventory);
                        }
                        break;
                    }
                }

            }
            OnItemListChanged?.Invoke(this, new ClassEventArgs());
        }

        public void UseItem(Item item)
        {
            useItemAction(item);
        }
        public List<Item> GetItemList()
        {
            return itemList;
        }

    }
}