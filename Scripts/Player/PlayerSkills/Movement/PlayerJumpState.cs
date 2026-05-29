using Player.Params;
using Player.PlayerStates.Movement;
using UnityEngine;

namespace Player.PlayerSkills.Movement
{
    public class PlayerJumpState : PlayerMovementBaseState
    {
        
        public PlayerJumpState(MovementManager movementManager, PlayerMovementBaseState stateMachine, PlayerMovementParams data)
        : base(movementManager, stateMachine, data) { }

        public PlayerMovementBaseState CurrentState { get; private set; }
        
        public override void EnterState(MovementManager playerState)
        {
            movementManager.Jump();
            movementManager.PlayJumpSound();
        }

        public override void UpdateState(MovementManager playerState)
        {

            SwitchState(movementManager.WalkState); 
            Debug.Log("Jump Update State");
        }
    
        public override void OnCollisionEnter(MovementManager playerState)
        {
        }
        
        public override void FixUpdate()
        {
            
        }
         
        
        

        
    }
}