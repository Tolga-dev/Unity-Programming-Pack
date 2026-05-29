using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Player.Inventory;
using PrimaryPlayer.PlayerComponentSkills.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


namespace PrimaryPlayer.PlayerComponentManagers.Inventory
{
    [Serializable]
    public class ItemWorldController : MonoBehaviour
    {
        
        [Header("Changeable")]
        public int amount;

        public ItemController ItemController;
         
        public int itemId;

        public TMP_Text itemTextObject;
        
        public Item dummyItem;
    }
    

    public class ItemWorldControllerHelper : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public LayerMask groundLayer; // Assign the ground layer in the Inspector
        public float detectionDistance;

        private void Start()
        {
            groundLayer = LayerMask.GetMask("Ground");
            
        }

        void Update()
        {
            
            // Check for collision with the ground
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, detectionDistance, groundLayer))
            {
                rigidbody.isKinematic = true;
                Destroy(rigidbody);
                Destroy(this);
            }
        }

    }
}