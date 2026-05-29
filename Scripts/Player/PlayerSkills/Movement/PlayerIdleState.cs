using System;
using Player.Params;
using Player.PlayerStates.Movement;
using UnityEngine;

namespace Player.PlayerSkills.Movement
{
    public class PlayerIdleState : PlayerMovementBaseState
    {
        public PlayerIdleState(MovementManager movementManager, PlayerMovementBaseState stateMachine, PlayerMovementParams data)
            : base(movementManager, stateMachine, data) { }

        public PlayerMovementBaseState CurrentState { get; private set; }

        public override void EnterState(MovementManager playerState)
        {
            
        }
        
        public override void UpdateState(MovementManager playerState)
        {
            Debug.Log("Idle Update State");
            if(movementManager.MoveInput.x != 0 || movementManager.MoveInput.z != 0) //change state to runnig, when moveInput detected
            {
                SwitchState(playerState.WalkState);
            }
            
        }
        
        public override void FixUpdate()
        {
            
        }
    
        public override void OnCollisionEnter(MovementManager playerState)
        {
            
        }

         
        
    }
}