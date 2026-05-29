using System;
using System.Collections.Generic;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;
using Player.PlayerSkills;
using Player.PlayerSkills.Defence;

namespace Player.PlayerStates.Defence
{
    public class DefenceManager : MonoBehaviour
    {
        
        [SerializeField] public PlayerDefenceParams Defencedata;
        [SerializeField] public PlayerMovementParams Movementdata;
        public PlayerDefenceBaseState _currentState;
        List<GameObject> enemyCollisionGameObjects = new List<GameObject>(); 
        public PlayerBlockState PlayerBlockState;
        public PlayerIdleState PlayerIdleState;
        
        public Animator playerAnimator;
        public LayerMask layers;
        
        [SerializeField] private AudioClip defenceSound;
        private AudioSource m_AudioSource;
        
        float resistance; // formul icin kullanilior gidici
        private void Awake()
        {
            PlayerBlockState = new PlayerBlockState(this, _currentState, Defencedata);
            PlayerIdleState = new PlayerIdleState(this, _currentState, Defencedata);
        }

        private void Start()
        {
            Defencedata.armor = 100;
            Movementdata.health = 100;
            m_AudioSource = GetComponent<AudioSource>();
            playerAnimator = GetComponent<Animator>(); 
            var controller = GetComponent<CharacterController>();
            
            _currentState = PlayerIdleState;
            _currentState.EnterState(this);
        } 
        private void Update()
        {
            
            InputCheck();
            _currentState.UpdateState(this);
             ;
        }

        private void InputCheck()
        {
            
            if(Input.GetKey(Inputs.OnGuard))
               _currentState.SwitchState(PlayerBlockState);
            else
                if (_currentState != PlayerIdleState)
                {
                    _currentState.SwitchState(PlayerIdleState);
                }

        }
        
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // 10 = ENEMY ATACK
            if (hit.collider.gameObject.CompareTag("enemy") && !enemyCollisionGameObjects.Contains(hit.gameObject))
            {

                if (_currentState == PlayerBlockState)
                    resistance = 1;
                else
                    resistance = 0;
                Defencedata.armor -= 10;
                Movementdata.health -= 10 - resistance;
                enemyCollisionGameObjects.Add(hit.gameObject);
                
                Debug.Log(Movementdata.health);
                Debug.Log(Defencedata.armor);
                
            }
            
        }

        float CalculateDamageByEnemy(float enemyAttack)
        {
            return enemyAttack;
        }
        float CalculateArmourDamageByEnemy(float enemyAttack)
        {
            enemyAttack /= Defencedata.armor * 10; 
            return Defencedata.armor > enemyAttack ? (enemyAttack) : 0;
        }

    }
}