using System;
using System.Collections;
using Player.Inventory;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;
using Player.PlayerSkills;
using Player.PlayerSkills.Attack;
using Player.PlayerStates.Movement;

namespace Player.PlayerStates.Attack
{
    public class AttackManager : MonoBehaviour
    {
        
        [SerializeField] private WeaponManager _weaponManager;
        [SerializeField] public PlayerAttackParams data;
        
        private MovementManager _movementManager;
        public PlayeraAttackBaseState _currentState;

        public PlayerShootState PlayerShootState;
        public PlayerPunchState PlayerPunchState;
        public PlayerAttackIdleState PlayerAttackIdleState;

        //public Transform shootPlaceTransform;
        public bool OnSelectingTarget = false;
        public Transform SelectingTargetExec;
        public Transform SelectingTargetCool;
        public GameObject Targeter;
        public Vector3 SelectingTargetAnimationBoneOffset;
        private Transform SelectingTargetAnimationBone;
        public RaycastHit hitForCameraAtes;

        public RuntimeAnimatorController onFireAnim;
        public RuntimeAnimatorController onIdleFireAnim;

        public GameObject AmmoPrefab;
        public Transform FirePlace;
        private Coroutine InMindAnim = null;

        public GameObject[] ammoPool = new GameObject[10];
        private int CreatedAmmoObj;
        public Animator playerAnimator;
        private Camera mainCamera;
        static bool isOpenTargeter = false;
        private void Awake()
        {
            PlayerShootState = new PlayerShootState(this, _currentState,data); // animasyon ile yaptim daa i
            PlayerAttackIdleState = new PlayerAttackIdleState(this, _currentState, data);
            _weaponManager.SetPlayerAttackManager(this);
        }
        
        private void Start()
        {
            _movementManager = GetComponent<MovementManager>();
            playerAnimator = GetComponent<Animator>();
            mainCamera = Camera.main;
            SelectingTargetAnimationBone = playerAnimator.GetBoneTransform(HumanBodyBones.Chest); // hata kaynag
            
            _currentState = PlayerAttackIdleState;
            _currentState.UpdateState(this); 
            CreateAmmoPool();
        } 
        
        private void LateUpdate()
        {
            
            if (OnSelectingTarget && hitForCameraAtes.distance > 2)
            {
                SelectingTargetAnimationBone.LookAt(hitForCameraAtes.point);
                SelectingTargetAnimationBone.rotation = SelectingTargetAnimationBone.rotation *
                                                        Quaternion.Euler(SelectingTargetAnimationBoneOffset);
            }
            
        }

        private void FixedUpdate()
        {
            
            if (OnSelectingTarget)
            {
                playerAnimator.runtimeAnimatorController = onFireAnim;
                mainCamera.gameObject.transform.position = Vector3.Lerp(mainCamera.gameObject.transform.position,
                    SelectingTargetExec.transform.position, 0.1f);
            }
            else
            {
                playerAnimator.runtimeAnimatorController = onIdleFireAnim;
                mainCamera.gameObject.transform.position = Vector3.Lerp(mainCamera.gameObject.transform.position,
                    SelectingTargetCool.transform.position, 0.1f);
            }
        }

        private void Update()
        {
            
            InputCheck();
            
            _currentState.UpdateState(this);
            
        }
        private void InputCheck()
        {
            if (Input.GetKeyDown(Inputs.Reload))
            {

                foreach (Item item in _movementManager.inventory.itemList)
                {
                    
                    if (item.itemTypes == Item.ItemTypes.Ammo)
                    {
                        _movementManager._weaponManager.ReloadGun(item);
                        break;
                    }
                }
                
            }

            if (Input.GetKeyDown(Inputs.SelectTarget))
            {
                OnSelectingTarget = true;
                StartCoroutine(WaiterAnimTargeter());
                // animasyon bitince caliscak bura ona gore bisi yacak
            }
            else if (Input.GetKeyUp(Inputs.SelectTarget))
            {

                OnSelectingTarget = false;
                Targeter.SetActive(false);                
            }            
            if (Input.GetKeyDown(Inputs.Shoot))
            {
                if( playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                {
                    Debug.Log(playerAnimator.name);
                    playerAnimator.speed = data.playerAttackShootAnimSpeed;
                }
                
                if(InMindAnim != null)
                    StopCoroutine(InMindAnim);
                playerAnimator.SetBool("OnFire", true);
            }
            else if (Input.GetKeyUp(Inputs.Shoot))
            {
                InMindAnim = StartCoroutine(FireAnimManagerStopping());
                
            }
        }
        
        void CreateAmmoPool()
        {
            GameObject AmmoObj;
            for (int i = 0; i < ammoPool.Length; i++)
            {
                AmmoObj = Instantiate(AmmoPrefab);
                AmmoObj.SetActive(false);
                ammoPool[i] = AmmoObj;

            }
        }
        
        
        public void OnFireAnim()
        {
            Debug.Log("fire anim");
            ammoPool[CreatedAmmoObj].SetActive(true);
            ammoPool[CreatedAmmoObj].transform.position = FirePlace.position;
            ammoPool[CreatedAmmoObj].transform.rotation = FirePlace.rotation;
            Rigidbody ammoRb = ammoPool[CreatedAmmoObj++].GetComponent<Rigidbody>();

            ammoRb.velocity = Vector2.zero;
            ammoRb.AddForce((hitForCameraAtes.point - FirePlace.transform.position).normalized * 3000);
            
            if (CreatedAmmoObj == ammoPool.Length)
            {
                CreatedAmmoObj = 0;
            }

        }
        

        IEnumerator WaiterAnimTargeter()
        {
            yield return new WaitForSeconds(0.3f);
            Targeter.SetActive(true);
            if (!OnSelectingTarget) Targeter.SetActive(false);
        }
        
        IEnumerator FireAnimManagerStopping()
        {
            yield return new WaitForSeconds(0.2f);
            playerAnimator.SetBool("OnFire", false);
        }

        


    }
}