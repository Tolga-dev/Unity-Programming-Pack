using System;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;
using PrimaryPlayer;
using PrimaryPlayer.GameEngine;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Inventory;
using UnityEngine.Serialization;


namespace PrimaryPlayer.PlayerManager
{
    public class PlayerManager : MonoBehaviour
    {
        
        
        [Header("Managers")]
        public MovementManager movementManager;
        public AttackManager attackManager;
        
        [Header("Animators")]
        public Animator animator;
        
        [Header("Components")]
        public UnityEngine.Camera mainCamera;
        
        [Header("Controllers")]
        [SerializeField] public CharacterController playerController;
        public KeyCodeManager InputKeyCodeManager;
        public InventoryManager inventor;

        [Header("Player Public Parameters")]
        public float health = 100;
        public bool isPlayerDead = false; 
        

        private void Start()
        {
            animator = GetComponent<Animator>();
            
            movementManager = GetComponent<MovementManager>();
            attackManager = GetComponent<AttackManager>();
            inventor = GetComponent<InventoryManager>();
            InputKeyCodeManager = new KeyCodeManager();
        
        }

        public void Dead() // maybe, in the future called from dead animation
        {
            movementManager.enabled = false;
            attackManager.enabled = false;
        }
    }
}