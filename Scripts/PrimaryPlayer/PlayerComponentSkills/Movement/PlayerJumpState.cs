using System;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;


namespace PrimaryPlayer.PlayerSkills.Movement
{
    public class PlayerJumpState : PlayerMovementBaseState
    {
        
        public PlayerJumpState(MovementManager movementManager,
            PlayerMovementBaseState playerMovementBaseState
        ) : base(movementManager,playerMovementBaseState)
        { 
            
        }

        public override void EnterState(MovementManager movementState)
        {
            
//            Debug.Log("Enter Jump State");
          //  movementState.playerMovement.y = Mathf.Sqrt(movementState.powerjump *-1f * -9.81f);
          
          if (movementState._mIsGrounded)
          {
//              Debug.Log("Can Jump!");
//                  Debug.Log("Jump Pow Added");
                  movementState.playerAirMovement.y = movementState.PowerJump; // 1 meters 
                  if (movementState.isOnWalkAnimation)
                      movementState.playerAirMovement.y *= movementState.PowerWalk;
                  else 
                      movementState.playerAirMovement.y *= movementState.PowerRun * 2;
                  
                  movementState.PlayerManager.animator.applyRootMotion = false;
          }
          else
          { 
//              Debug.Log("Cannot Jump!");
          }
          
            ExitState(movementState);
        }
            

        public override void UpdateState(MovementManager movementState)
        {
            
           // Debug.Log("Update Jump State");
        }
        
        public override void ExitState(MovementManager movementState)
        {
         //   Debug.Log("Exited Jump State");
            movementState.CurrentState = movementState.WalkState;
        }

        
    }
}