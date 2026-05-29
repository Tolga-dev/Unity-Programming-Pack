using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Microsoft.Unity.VisualStudio.Editor;
using Player.PlayerInputLists;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;
using Player.PlayerSkills;
using Player.PlayerSkills.Movement;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;
using Player.Inventory;
using Player.PlayerStates.Attack;

namespace Player.PlayerStates.Movement
{
    public class MovementManager : MonoBehaviour
    {
        
        [SerializeField] public PlayerMovementParams data;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] public WeaponManager _weaponManager;
      
        public PlayerMovementBaseState _currentState;
        public AttackManager _attackManager;
        public PlayerJumpState JumpState;
        public PlayerWalkState WalkState;
        public PlayerIdleState IdleState;
        public PlayerRunState RunState;
   //    public PlayerGetInCarState GetInCarState;
        public Inventory.Inventory inventory;
        
        public Vector3 MoveInput;
        public Vector3 Velocity;
        
        private CapsuleCollider playerCapsuleCollider;
        
        public Animator playerAnimator;
        public RuntimeAnimatorController MovementAnims;
        public PlayerCamera playerCamera;
        public CharacterController Controller;
        public Transform groundCheck;
        public LayerMask layers;
        private bool m_PreviouslyGrounded = false; 
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;   
        private AudioSource m_AudioSource;
        public bool isOpenedInventory = false;
        public Transform carGetInTransformSit;
        
        public bool CanMoveInsideTheCar;
        public bool onDrive = false;
        private void Awake()
        {
            JumpState  = new PlayerJumpState(this,_currentState,data );
            WalkState  = new PlayerWalkState(this,_currentState,data );
            IdleState  = new PlayerIdleState(this,_currentState,data );
            RunState  = new PlayerRunState(this,_currentState,data );
         //   GetInCarState  = new PlayerGetInCarState(this,_currentState,data );
            inventory = new Inventory.Inventory(UseItem);
            _uiManager.SetInventory(inventory);
            _uiManager.SetPlayer(this);
            _weaponManager.SetPlayer(this);

        }

        private void OnTriggerEnter(Collider other)
        {
            ItemsWorldController itemsWorldController = other.GetComponent<ItemsWorldController>();
            if (itemsWorldController != null)
            {
                inventory.AddItem(itemsWorldController.GetItem());
             //   _weaponManager.AddToItemsTransformForItems(itemsWorldController.GetItem());
                _weaponManager.AddToItemsTransformForItems2(itemsWorldController);
                
              //  itemsWorldController.DestroySelf();
            }
        }

        private void Start()
        {
            playerCamera = GetComponent<PlayerCamera>();
            _attackManager = GetComponent<AttackManager>();
            m_AudioSource = GetComponent<AudioSource>();
            playerAnimator = GetComponent<Animator>();
            playerCapsuleCollider = GetComponent<CapsuleCollider>();
            Controller = GetComponent<CharacterController>();
            
            _currentState = IdleState;
            _currentState.UpdateState(this);
            playerAnimator.SetBool("Idle",true);
            
        }

        private void Update()
        {
            if (!onDrive)
            {
                InputCheck();
                
                if (data.CanJump && Velocity.y < 0)
                {
                    Velocity.y = -2f;
                }

                Velocity.y += data.Gravity * Time.deltaTime;
                
                Controller.Move(Velocity * Time.deltaTime);
                
                _currentState.UpdateState(this);
            }
            Debug.Log(MoveInput);
            /*
             * all requests will be collected into an IEnumerator queue, if there are no requests (queue empty) it will remain idle
             * so we can escape the necessary conditioning for idle, more performance
             */
            
        }
        
        private void InputCheck()
        {
            data.CanJump =  Physics.CheckSphere(groundCheck.position, data.groundDistance, layers);
             
            if(data.CanJump)
            {
                MoveInput.x = Input.GetAxis("Horizontal");
                MoveInput.y = 0;
                MoveInput.z = Input.GetAxis("Vertical");
                MoveInput = transform.TransformDirection(MoveInput);
                MoveInput.Normalize();

                playerAnimator.SetFloat("Horizontal",MoveInput.x);
                playerAnimator.SetFloat("Vertical",MoveInput.z);
                
                if(MoveInput.magnitude == 0)
                    playerAnimator.SetBool("Idle",true);
                else
                {
                    playerAnimator.SetBool("Idle",false);
                    PlayFootStepAudio();
                }
                
                if (_currentState == RunState)
                {
                    playerAnimator.SetBool("Run",true);
                }
                else
                {
                    playerAnimator.SetBool("Run",false);
                }

                if (m_PreviouslyGrounded)
                {
                    m_PreviouslyGrounded = false;
                    PlayLandingSound();
                }
                    
            }
            
            if (Input.GetKeyDown(Inputs.Jump) && data.CanJump)
            {

                Debug.Log("hello");
                playerAnimator.SetBool("Jump",true);
                m_PreviouslyGrounded = true;
                _currentState.SwitchState(JumpState);
                
            }

            if (Input.GetKeyUp(Inputs.Inventory))
            {
                isOpenedInventory = !isOpenedInventory;
                if (isOpenedInventory)
                    _uiManager.OpenInventory();
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    _uiManager.CloseInventory();
                }
            }
    
        }

        private void UseItem(Item item)
        {
            
            _weaponManager.AddToItemsForSelectedItems(item);
            switch (item.itemTypes)
            {
                case Item.ItemTypes.Gun:
                    Debug.Log("Gun is Used");
 
                    break;
                case Item.ItemTypes.Grenade:
                    Debug.Log("Grenade is Used");
                    // inventory.RemoveItem(new Item { itemTypes = Item.ItemTypes.Grenade, amount = 1 });
                    break;
            }
        }

        public bool IsThereInput()
        {
            return MoveInput.x != 0 && MoveInput.z != 0 ? true : false;
        }

        public void Walk()
        {
            Controller.Move( MoveInput* data.WalkVelocity * Time.deltaTime);
        }
         
        public void Run()
        {
            Controller.Move(MoveInput * data.RunVelocity * Time.deltaTime);
        }
        
        public void Jump()
        {
            // it is done by animation event but does not work :/
            Velocity.y = Mathf.Sqrt(data.JumpForce * -1f * data.Gravity);
            
        }
        
        public void PlayJumpSound()
        {
            
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
        private void PlayFootStepAudio()
        {
            
            if (!data.CanJump )
            {
                return;
            }

            if (!m_AudioSource.isPlaying)
            {
                // pick & play a random footstep sound from the array,
                // excluding sound at index 0
                int n = Random.Range(1, m_FootstepSounds.Length);
                m_AudioSource.clip = m_FootstepSounds[n];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
                
                // move picked sound to index 0 so it's not picked next time
                m_FootstepSounds[n] = m_FootstepSounds[0];
                m_FootstepSounds[0] = m_AudioSource.clip;
                
            }

        }

        public void JumpParamFalse()
        {
            playerAnimator.SetBool("Jump",false);
        }

        public void JumpAddVelocity() // simdilik kalacak animasyon ayarlerken artik
        {
           // Velocity.y = Mathf.Sqrt(data.JumpForce * -1f * data.Gravity);
        }
        
        
        public void EnteringFromCarEvents() // enter the car event, wikl change the name 
        {
            CanMoveInsideTheCar = true;
            
        }

        public void DrivingTheCar()
        {

            transform.rotation = Quaternion.Euler(0,90,0);
            if (!onDrive)
            {
                
                transform.parent.GetComponent<CarController>().carAnimController.SetBool("DoorAnimStart", false);
                _attackManager.enabled = false;
                transform.parent.GetComponent<CarController>().PlayerGotInTheCar = true;
                transform.parent.GetComponent<CameraCarController>().enabled = true;
                onDrive = true;
                Controller.enabled = false;
                playerCamera.enabled = false;
                
            }

        }
        

        public void ExitingFromTheCarEvents()
        {
            playerCamera.enabled = true;
            _attackManager.enabled = true;        
            transform.parent.GetComponent<CarController>().isPlayerHere.SetActives();
            transform.parent.GetComponent<CarController>().isPlayerHere.ResetTransformTargetPosesValues();
            CanMoveInsideTheCar = false;
            transform.parent.GetComponent<CarController>().PlayerGotInTheCar = false;
            transform.parent.GetComponent<CameraCarController>().enabled = false;
            
          
            playerAnimator.SetBool("StartCarLayer",false);
          

            transform.parent.GetComponent<CarController>().carAnimController.SetBool("DoorAnimStart", true);
            gameObject.transform.parent = transform.parent.GetComponent<CarController>().PlayerParentPlayerTransform.transform;
            MoveInput.y = 0;
            Debug.Log("exiting from the car");
            
        }

        public void ExitingFromTheCarEventsFrontPreparation()
        {
            Debug.Log(transform.parent.GetComponent<CarController>().PlayerParentPlayerTransform);
            transform.parent.GetComponent<CarController>().PlayerTheCarOnDoor = false;
            onDrive = false;
            Controller.enabled = true;
            playerAnimator.SetBool("GoToCar", false);
        }
        
    }
}