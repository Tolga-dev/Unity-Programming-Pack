using System;
using System.Collections;
using Input_Update_Attack;
using PrimaryPlayer.GameEngine;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using PrimaryPlayer.PlayerComponentSkills.Attack;
using PrimaryPlayer.PlayerSkills.Attack;
using UnityEngine;
using UnityEngine.Serialization;
using PlayerIdleState = PrimaryPlayer.PlayerSkills.Attack.PlayerIdleState;

namespace PrimaryPlayer.PlayerComponentManagers.Attack
{
    public class AttackManager : MonoBehaviour
    {
        [SerializeField] public PlayerManager.PlayerManager playerManager;
        [SerializeField] public MovementManager movementManager;
        [SerializeField] public KeyCodeManager KeyCodeManager;
        [SerializeField] public PlayerAttackBase CurrentState;
        [SerializeField] public InputManager InputManager;
    
        public PlayerIdleState PlayerIdleState;
        public PlayerRifleState PlayerRifleState;
        public PlayerPunchState PlayerPunchState;
        public PlayerGrenadeState PlayerGrenadeState;
        
        private UnityEngine.Camera mainCamera;
        
        //animation controller
        public Animator playerAnimator;

        // animation bones
        private Transform _selectingTargetAnimationBone;
        public Vector3 selectingTargetAnimationBoneOffset; // max min bone movement
        
        // On Targeting Idle        
        public bool onSelectingTarget = false;
        // calculate distance 
        public RaycastHit HitForTargetPlace;
        
        // animation checks
        public bool OnFire = false; 
        
        // Camera Aiming Transmform
        public Transform SelectingTargetOn;
        public Transform SelectingTargetCool;
        public Transform SelectingTargetOnCrouch;
        public GameObject TargeterRedPoint;
        
        public GameObject AmmoPrefab;
        public Transform GunFirePlace;
        
        private Coroutine InMindAnim = null;
        public float PowerAmmo = 10000f;

        
        private void Start()
        {
            playerManager = GetComponent<PlayerManager.PlayerManager>();
            movementManager = GetComponent<MovementManager>();
            KeyCodeManager = new KeyCodeManager();
            playerAnimator = GetComponent<Animator>();
            InputManager = new InputManager(this);
            
            mainCamera = playerManager.mainCamera;
            _selectingTargetAnimationBone = playerAnimator.GetBoneTransform(HumanBodyBones.Chest); 
            CurrentState.EnterState(ref playerManager.attackManager);
        }
        
        private void Awake()
        {
            PlayerIdleState = new PlayerIdleState(this, movementManager,CurrentState);
            PlayerRifleState = new PlayerRifleState(this, movementManager,CurrentState);
            PlayerPunchState = new PlayerPunchState(this, movementManager,CurrentState);
            PlayerGrenadeState = new PlayerGrenadeState(this, movementManager,CurrentState);
                                              
            CurrentState = PlayerPunchState;
        }
        
        private void LateUpdate()
        {
            CameraOnSelectingRotBodyMovement();
        }
        
        private void FixedUpdate()
        {
            if (onSelectingTarget)
            {
                ActionOnSelectingTarget();
            }
            else
            {
                ActionUnSelectingTarget();
            }

        }
        
        private void Update()
        {
            CurrentState.UpdateState(ref playerManager.attackManager);
        }
        

        public void InputActionOnFireDown()
        {
            if(InMindAnim != null)
                StopCoroutine(InMindAnim);
                
            OnFire = true;
            UpdateAnimation("OnFire",OnFire);
        }

        public void InputActionOnFireUp()
        {
            OnFire = false;
            InMindAnim = StartCoroutine(FireAnimationStopping());
        }

        private IEnumerator FireAnimationStopping()
        {
            yield return new WaitForSeconds(0.2f);
            UpdateAnimation("OnFire",false);
        }
        
        private void UpdateAnimation(string name, bool animationIdentifier)=> playerAnimator.SetBool(name, animationIdentifier);
        
        private Vector3 GetSelectingTargetOnTransform() => Vector3.Lerp(mainCamera.gameObject.transform.position,
            SelectingTargetOn.transform.position, 0.1f);
        
        private Vector3 GetSelectingTargetCoolTransform() => Vector3.Lerp(mainCamera.gameObject.transform.position,
            SelectingTargetCool.transform.position, 0.1f);
        
        private Vector3 GetSelectingTargetCrouchTransform() => Vector3.Lerp(mainCamera.gameObject.transform.position,
            SelectingTargetOnCrouch.transform.position, 0.1f);
        

         // OnFire animation event
        public void OnFireAnim()
        {
            //  Debug.Log("fire anim");
            
            var ammo = Instantiate(AmmoPrefab, GunFirePlace.position, Quaternion.identity);
            ammo.gameObject.transform.rotation = GunFirePlace.rotation;
            var ammoRb = ammo.GetComponent<Rigidbody>();

            ammoRb.velocity = Vector2.zero; 
            ammoRb.AddForce((HitForTargetPlace.point - GunFirePlace.transform.position).normalized * PowerAmmo, ForceMode.Impulse);
            
        }


        public void SpawnedMe()
        {
            var ammo = Instantiate(AmmoPrefab, transform.position, Quaternion.identity);

        }
        public void SetGrenadeAnimationFalse()
        {
            playerAnimator.SetBool("OnGrenade", false);
        }
        

        private void CameraOnSelectingRotBodyMovement() // camera on selected state 
        {
            if (onSelectingTarget && HitForTargetPlace.distance > 2)
            {
                _selectingTargetAnimationBone.LookAt(HitForTargetPlace.point);
                _selectingTargetAnimationBone.rotation *= Quaternion.Euler(selectingTargetAnimationBoneOffset);
            }
            
        }
        private void ActionUnSelectingTarget()
        {
            mainCamera.gameObject.transform.position = GetSelectingTargetCoolTransform();            
        }

        private void ActionOnSelectingTarget()
        {
            mainCamera.gameObject.transform.position =
                movementManager._playerOnCrouch ? 
                    GetSelectingTargetCrouchTransform() :
                    GetSelectingTargetOnTransform();
        }

        public void InputActionOnSelectingTarget()
        {
            onSelectingTarget = true;
            UpdateAnimation("OnAiming",onSelectingTarget);
            StartCoroutine(WaitTargetedOpening());
        }
        public void InputActionUpSelectingTarget()
        { 
            onSelectingTarget = false;
            UpdateAnimation("OnAiming",onSelectingTarget);
            TargeterRedPoint.SetActive(false);
        }
        private IEnumerator WaitTargetedOpening()
        {
            yield return new WaitForSeconds(0.3f);
            TargeterRedPoint.SetActive(true); 
        }
        
        
        
    }
}

namespace Input_Update_Attack
{
    public class InputManager
    {
        private AttackManager _manager;
        private KeyCodeManager _keyCodeManager;

        public InputManager(AttackManager attackManager)
        {
            _manager = attackManager;
            _keyCodeManager = _manager.KeyCodeManager;
            
        }

        public bool SelectingTargetDown() => Input.GetKeyDown(_keyCodeManager.Keycode.SelectTarget);
        public bool SelectingTargetUp() => Input.GetKeyUp(_keyCodeManager.Keycode.SelectTarget);
        public bool OnFireDown() => Input.GetKeyDown(_keyCodeManager.Keycode.Shoot);
        public bool OnFireUp() => Input.GetKeyUp(_keyCodeManager.Keycode.Shoot);

        
    }
    
}

